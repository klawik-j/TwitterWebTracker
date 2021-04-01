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
