using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        [BindProperty(SupportsGet = true)]
        public string TwitterUserName { get; set; }
        public HttpClient Client { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Contex { get; set; }

        public async Task OnGet()
        {
            var Obj = new TwitterApiProcessor(_config, TwitterUserName);
            await Obj.GetUserID();
            Contex = Obj.Contex;
            /*
            string url = "";
            if (string.IsNullOrWhiteSpace(TwitterUserName))
            {
                url = "https://api.twitter.com/2/users/by/username/LanaRhoades";
            }
            else
            {
                url = $"https://api.twitter.com/2/users/by/username/{ TwitterUserName }";
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
            */
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            return RedirectToPage("/Index", new { TwitterUserName });
        }
    }
}
