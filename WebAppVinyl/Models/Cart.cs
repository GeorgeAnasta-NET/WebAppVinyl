using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppVinyl.Models
{
    public class Cart
    {
        public int Id { get; set; }
        //[Key]
        //[Column(Order = 1)]
        public int VinylId { get; set; }

        //[Key]
        //[Column(Order = 2)]
        public string BuyerId { get; set; }

        // navigation properties
        public Vinyl Vinyl { get; set; }
        public ApplicationUser Buyer { get; set; }

        public int? Quantity { get; set; }
        
    }
}