using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1DataAccessLibrary.Models
{
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
