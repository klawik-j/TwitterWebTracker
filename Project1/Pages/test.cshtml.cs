using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projet1DataAccessLibrary.DataAccess;
using Projet1DataAccessLibrary.Models;

namespace Project1.Pages
{
    public class testModel : PageModel
    {
        private readonly Projet1DataAccessLibrary.DataAccess.TwittContext _context;

        public testModel(Projet1DataAccessLibrary.DataAccess.TwittContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DBTwitt DBTwitt { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Twitt.Add(DBTwitt);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
