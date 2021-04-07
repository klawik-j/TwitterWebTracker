using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Klasa obslugujaca API Twittera. Zawiera metody niezbednedo do prawidlowego wysylania i odbierania zapytan o dane.
    /// </summary>
    /// 
    /// <item>
    /// <term>_config</term>
    /// <description>Zmnienna konfiguracyjna uzywana w celu obslugi secret'ow takich jak token API przechowywanych w pliku srodowiskowym,
    /// bez koniecznosci ujawniania go bezposrednio w kodzie</description>
    /// </item>
    /// 
    /// <item>
    /// <term>_logger</term>
    /// <description>Zmienna uzyta w celu wykorzystania narzedzia logger w celu debugowania kodu podczas jego pisania i testowania</description>
    /// </item>
    /// 
    /// <item>
    /// <term>_TwitterUserName</term>
    /// <description>Zmienna bedaca nazwa uzytkowanika ktory podlega wyszukaniu w zapytaniu API</description>
    /// </item>
    /// 
    /// <item>
    /// <term>User</term>
    /// <description>Zmienna zawierajaca zdeserializowana odpowiedz o uzytkownika z API Twittera</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Twitts</term>
    /// <description>Zmienna zawierajaca zdeserializowana odpowiedz o twitty konkretnego uzytkownika z API Twittera</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Contex</term>
    /// <description>Zmienna przechowywujaca tresc ewentualnego bledu zapytania API ktory w jakis sposob nie zostal obsluzony przez assercie</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Client</term>
    /// <description>Zmienna stanowiaca strone klienta przy obsludze zapytan i odpowiedzi o API Twittera</description>
    /// </item>
    /// 
    /// <item>
    /// <term>options</term>
    /// <description>Zmienna okreslajaca ustawienia deserializera, w tym konkretnym przypasku ignoruje on roznice pomiedzy wielkoscia liter w nazwach zmiennych</description>
    /// </item>
    /// 
    /// <item>
    /// <term>makeUserUrl</term>
    /// <description>Funckja tworzaca prawidlowe url zapytania o dane uzytkowika Twittera na podstawie jego username</description>
    /// </item>
    /// 
    /// <item>
    /// <term>GetUserID</term>
    /// <description>Funkcja obslugujaca zapyanie API Twittera o konkretnego uzytkownika.</description>
    /// </item>
    /// 
    /// <item>
    /// <term>makeTwittUrl</term>
    /// <description>Funckja tworzaca prawidlowe url zapytania o liste 15 ostatnich twittow na podstawie jego Id konretnego uzytkownika</description>
    /// </item>
    /// 
    /// <item>
    /// <term>GetUserTwitts</term>
    /// <description>Funkcja obslugujaca zapyanie API Twittera o konkretnego uzytkownika. </description>
    /// </item>
    public class TwitterApiProcessor
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TwitterApiProcessor> _logger;
        private readonly string _TwitterUserName;
        public TwitterUser User { get; set; }
        public TwitterTwitts Twitts { get; set; }
        public string Contex;
        private HttpClient Client;
        private JsonSerializerOptions options;
        public TwitterApiProcessor(ILogger<TwitterApiProcessor> logger, IConfiguration config, string TwitterUserName)
        {
            _logger = logger;
            _config = config;
            _TwitterUserName = TwitterUserName;
            Client = new HttpClient();
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        /// <summary>
        /// Funckja tworzaca prawidlowe url zapytania o dane uzytkowika Twittera na podstawie jego username
        /// </summary>
        /// <param name="_TwitterUserName">string username uzytkownika</param>
        /// <returns>string url zapytania API</returns>
        /// <example>
        /// <code>
        /// url = makeUserUrl("POTUS")
        /// </code>
        /// </example>
        static public string makeUserUrl(string _TwitterUserName)
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(_TwitterUserName))
            {
                throw new ArgumentException("Username was not passed");
            }
            else
            {
                url = $"https://api.twitter.com/2/users/by/username/{ _TwitterUserName }?user.fields=profile_image_url";
                return url;
            }
        }

        /// <summary>
        /// Funkcja obslugujaca zapyanie API Twittera o konkretnego uzytkownika. 
        /// Funckja nie zwraca bezposrednio zadnej wartosci. 
        /// Modyfikuje ona na bierzaco warosci zmiennych klasy
        /// </summary>
        public async Task GetUserID()
        {
            var url = makeUserUrl(_TwitterUserName);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config["Twitter:BearerToken"]);
            var response = await Client.SendAsync(request);
            _logger.LogInformation("GetUserId():ResponseCode: {response_code}", response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("response:{data}", data);
                var responseStream = await response.Content.ReadAsStreamAsync();
                User = await JsonSerializer.DeserializeAsync<TwitterUser>(responseStream, options);
                _logger.LogInformation("{user}", User);
            }
            else if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("errors in reponse json: {User.Errors}", response.StatusCode);
                var responseStream = await response.Content.ReadAsStringAsync();
                Contex = responseStream;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Funckja tworzaca prawidlowe url zapytania o liste 15 ostatnich twittow na podstawie jego Id konretnego uzytkownika
        /// </summary>
        /// <param name="UserId">Numer identyfikacyjny uzytkownika</param>
        /// <returns>string url zapytania API</returns>
        /// <example>
        /// <code>
        /// url = makeTwittUrl(1377428415716950023)
        /// </code>
        /// </example>
        static public string makeTwittUrl(string UserId)
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(UserId))
            {
                throw new ArgumentException("User's id is null");
            }
            else
            {
                url = $"https://api.twitter.com/2/users/{ UserId }/tweets?tweet.fields=created_at,possibly_sensitive&max_results=15";
                return url;
            }
        }

        /// <summary>
        /// Funckja obslugujaca zapytania do API Twittera o liste ostatnich 15 twittow.  
        /// Funckja nie zwraca bezposrednio zadnej wartosci.
        /// Modyfukuje wartosci zmiennych w klasie.
        /// </summary>
        public async Task GetUserTwitts()
        {
            string url = makeTwittUrl(User.Data.Id);
          
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config["Twitter:BearerToken"]);
            var response = await Client.SendAsync(request);
            _logger.LogInformation("response status code: {status_code}", response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("response:{data}", data);
                var responseStream = await response.Content.ReadAsStreamAsync();
                Twitts = await JsonSerializer.DeserializeAsync<TwitterTwitts>(responseStream, options);
            }
            else
            {
                
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
