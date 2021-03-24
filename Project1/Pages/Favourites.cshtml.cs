using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projet1DataAccessLibrary.DataAccess;
using Projet1DataAccessLibrary.Models;

namespace Project1.Pages
{
    public class FavouritesModel : PageModel
    {
        private readonly Projet1DataAccessLibrary.DataAccess.TwittContext _context;

        public FavouritesModel(Projet1DataAccessLibrary.DataAccess.TwittContext context)
        {
            _context = context;
        }

        public IList<DBTwitt> DBTwitt { get;set; }
        [BindProperty]
        public DBTwitt TwittToDelete { get; set; }

        public async Task OnGetAsync()
        {
            DBTwitt = await _context.Twitt.OrderByDescending(p => p.Saved_at).ToListAsync();
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Twitt.Remove(TwittToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            return Page();
        }
    }
}
