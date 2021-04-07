using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1DataAccessLibrary.Models
{
    /// <summary>
    /// Klasa bedaca modelem w oparciu o kory stworzona jest struktura w bazie dancyh.
    /// </summary>
    /// 
    /// <item>
    /// <term>Id</term>
    /// <description>
    /// Numer identyfikacyjny
    /// Ograniczenia:
    /// - wymagane
    /// - maxymalna dlugosc: 100
    /// - rozdzaj zmiennej w bd: varchar(10)
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>Text</term>
    /// <description>
    /// Tresc Twitta
    /// Ograniczenia:
    /// - wymagane
    /// - maxymalna dlugosc: 350
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>Author</term>
    /// <description>
    /// Autor Twitta
    /// Ograniczenia:
    ///  - wymagane
    ///  -maxymalna dlugosc: 100
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>Profile_image_url</term>
    /// <description>
    /// url do zdjecia profilowego
    /// Ograniczenia:
    /// - wymagane
    /// - maxymalna dlugosc: 200
    /// - rodzaj zmiennej w bd: varchar(200)
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>Created_at</term>
    /// <description>
    /// Data kiedy Twitt zostal opublikowany
    /// Ograniczenia:
    /// - wymagane
    /// - rodzaj zmiennej w bd: datetime
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>Saved_at</term>
    /// <description>
    /// Ograniczenia:
    /// - wymagane
    /// - rodzaj zmiennej w bd: datetime
    /// </description>
    /// </item>
    public class DBTwitt
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Id { get; set; }
        [Required]
        [MaxLength(350)]
        public string Text { get; set; }
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Profile_image_url { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Created_at { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Saved_at { get; set; }
    }
}
