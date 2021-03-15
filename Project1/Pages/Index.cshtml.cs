using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty(SupportsGet = true)]
        public string TwitterUserName { get; set; }
        public HttpClient Client { get; set; }

        public async Task OnGet()
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(TwitterUserName))
            {
                url = "http://xkcd.com/info.0.json";
            }
            else
            {
                url = $"http://xkcd.com/{ TwitterUserName }/info.0.json";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var Client = new HttpClient();
            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                TwitterUserName = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
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
