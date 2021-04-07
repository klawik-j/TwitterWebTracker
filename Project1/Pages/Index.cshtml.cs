using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project1.Models;
using Projet1DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project1.Pages
{
    /// <summary>
    /// Klasa tworzaca i obslugujaca endnode /Index
    /// </summary>
    /// <item>
    /// <term>_logger</term>
    /// <description>Zmienna wykorzystywana w celu debugowania</description>
    /// </item>
    /// <item>
    /// <term>TwitterUserName</term>
    /// <description>Zmienna obslugujaca forms.
    /// Cechuje sie tym, ze jest wymagana, aby wykonac operacje submit na stronie,
    /// jak rowniez tym, ze przyjmuje wylacznie wartosci A-Z,a-z,0-9
    /// </description>
    /// </item>
    /// <item>
    /// <term>OnPost</term>
    /// <description>Funckja wywolywana gdy zostanie wykonana akcja submit na stronie.</description>
    /// </item>
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
        [RegularExpression(@"^[A-Z]+[a-zA-Z]+[0-9]*$")]
        [Required]
        public string TwitterUserName { get; set; }

        public void OnGet()
        {

        }

        /// <summary>
        /// Funckja wywolywana gdy zostanie wykonana akcja submit na stronie. 
        /// </summary>
        /// <returns>
        /// W przypadku poprawnego wypelnienia formsa, przenoszonym zostaje sie do endnodu /List
        /// W przypadku porazki, strona jest odswiezana.
        /// </returns>
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            return RedirectToPage("/List", new { TwitterUserName });
        }
    }
}
