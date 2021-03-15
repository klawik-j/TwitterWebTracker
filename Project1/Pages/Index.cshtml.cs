using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project1.Models;
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
        [BindProperty(SupportsGet = true)]
        public string Contex { get; set; }
        public TwitterTwitts Twitts { get; set; }

        public async Task OnGet()
        {
            var Obj = new TwitterApiProcessor(_config, TwitterUserName);
            await Obj.GetUserID();
            await Obj.GetUserTwitts();
            Twitts = Obj.Twitts;
          
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
