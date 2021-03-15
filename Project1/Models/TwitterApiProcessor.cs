using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project1
{
    public class TwitterApiProcessor
    {
        private readonly IConfiguration _config;
        private readonly string _TwitterUserName;
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
                Contex = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
