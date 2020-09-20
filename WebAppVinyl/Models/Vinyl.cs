using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppVinyl.Models
{
    public class Vinyl
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Artist { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        
        public DateTime ReleaseYear { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Label Label { get; set; }

        [Required]
        public int LabelId { get; set; }

        public int? Quantity { get; set; }
    }
}