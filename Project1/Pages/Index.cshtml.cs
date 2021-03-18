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
        private readonly ILogger<TwitterApiProcessor> _loggerTwitterApiProcessor;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;

        public IndexModel(ILogger<IndexModel> logger, ILogger<TwitterApiProcessor> loggerTwitterApiProcessor, IConfiguration config)
        {
            _logger = logger;
            _loggerTwitterApiProcessor = loggerTwitterApiProcessor;
            _config = config;
        }
        [BindProperty(SupportsGet = true)]
        public string TwitterUserName { get; set; }
        public string Contex = null;
        public TwitterTwitts Twitts = null;
        public TwitterUser User = null;

        public async Task OnGet()
        {
            var Obj = new TwitterApiProcessor(_loggerTwitterApiProcessor, _config, TwitterUserName);
            await Obj.GetUserID();
            if (Obj.Contex == null && Obj.User.Data.Id != null)
            {
                await Obj.GetUserTwitts();
                Twitts = Obj.Twitts;
                User = Obj.User;
            }
            else
            {
                Contex = Obj.Contex;
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
