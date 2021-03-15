using Microsoft.Extensions.Configuration;
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
        private readonly string _TwitterUserName;
        public TwitterUser User { get; set; }
        public string Contex;
        public TwitterApiProcessor(IConfiguration config, string TwitterUserName)
        {
            _config = config;
            _TwitterUserName = TwitterUserName;
        }
        public async Task GetUserID()
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(_TwitterUserName))
            {
                url = "https://api.twitter.com/2/users/by/username/LanaRhoades";
            }
            else
            {
                url = $"https://api.twitter.com/2/users/by/username/{ _TwitterUserName }";
            }
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config["Twitter:BearerToken"]);
            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                User = await JsonSerializer.DeserializeAsync<TwitterUser>(responseStream, options);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task GetUserTwitts()
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(User.Data.Id))
            {
                throw new Exception("User's id is null");
            }
            else
            {
                url = $"https://api.twitter.com/2/users/{ User.Data.Id }/tweets?tweet.fields=id";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config["Twitter:BearerToken"]);
            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Contex = await response.Content.ReadAsStringAsync();
                
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
