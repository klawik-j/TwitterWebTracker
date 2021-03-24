using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project1.Models;
using Projet1DataAccessLibrary.Models;

namespace Project1.Pages
{
    public class ListModel : PageModel
    {
        private readonly ILogger<TwitterApiProcessor> _loggerTwitterApiProcessor;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private readonly Projet1DataAccessLibrary.DataAccess.TwittContext _context;

        public ListModel(ILogger<IndexModel> logger, ILogger<TwitterApiProcessor> loggerTwitterApiProcessor, IConfiguration config, Projet1DataAccessLibrary.DataAccess.TwittContext context)
        {
            _logger = logger;
            _loggerTwitterApiProcessor = loggerTwitterApiProcessor;
            _config = config;
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string TwitterUserName { get; set; }
        public string Contex = null;
        public TwitterTwitts Twitts = null;
        public TwitterUser User = null;
        [BindProperty]
        public DBTwitt DBTwitt { get; set; }
        public List<string> ListOfTwittIdInDB { get; set; }

        public async Task OnGet()
        {
            _logger.LogInformation("List:OnGet()::TwitterUserName: {TwitterUserName}", TwitterUserName);
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
            ListOfTwittIdInDB = _context.Twitt.Select(c => c.Id).ToList();
            _logger.LogInformation("List:OnGet()::ListOfTwittIdInDB: {ListOfTwittIdInDB}", ListOfTwittIdInDB);
        }
        public void OnPost()
        {

        }
        public async Task<IActionResult> OnPostCreateAsync(string UserName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Twitt.Add(DBTwitt);
            await _context.SaveChangesAsync();

            _logger.LogInformation("List:OnPostCreateAsync():TwitterUserName: {TwitterUserName}", TwitterUserName);
            TwitterUserName = UserName;
            return RedirectToPage("/List", new { TwitterUserName });
        }
        public async Task<IActionResult> OnPostDeleteAsync(string UserName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Twitt.Remove(DBTwitt);
            await _context.SaveChangesAsync();

            _logger.LogInformation("List:OnPostCreateAsync():TwitterUserName: {TwitterUserName}", TwitterUserName);
            TwitterUserName = UserName;
            return RedirectToPage("/List", new { TwitterUserName });
        }
    }
}
