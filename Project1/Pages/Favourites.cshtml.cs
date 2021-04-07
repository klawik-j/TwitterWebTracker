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
    /// <summary>
    /// Klasa tworzaca i obslugujaca endnode /Favourites
    /// </summary>
    /// <item>
    /// <term>_context</term>
    /// <description>Zmienna do odwolywania sie do bazy danych</description>
    /// </item>
    /// 
    /// <item>
    /// <term>DBTwitt</term>
    /// <description>
    /// Zmienna klasy DBTwitt bedaca modelem danych przechowywanych w bazie danych.
    /// Przechowuje liste Twittow dodanych do ulubionych
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>TwittToDelete</term>
    /// <description>Zmienna obslugujaca forms sluzacy do usuwania danych z bazy danych</description>
    /// </item>
    /// 
    /// <item>
    /// <term>OnGetAsync</term>
    /// <description>Funckja wywolywana asynchronicznie podczas zapytania GET na endpode /Favourites</description>
    /// </item>
    /// 
    /// <item>
    /// <term>OnPostRemoveAsync</term>
    /// <description>Funckja wywolywana asynchronicznie po nacisnieciu przycisku REMOVE</description>
    /// </item>
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

        /// <summary>
        /// Funckja wywolywana asynchronicznie podczas zapytania GET na endpode /Favourites.
        /// Nadaje wartosc zmiennej DBTwitt niezbednej do wyswietlenia listy.
        /// </summary>
        public async Task OnGetAsync()
        {
            DBTwitt = await _context.Twitt.OrderByDescending(p => p.Saved_at).ToListAsync();
        }
        /// <summary>
        /// Funckja wywolywana asynchronicznie po nacisnieciu przycisku REMOVE.
        /// </summary>
        /// <returns>
        /// Odswieza aktualnie wyswietlana strone
        /// </returns>
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
