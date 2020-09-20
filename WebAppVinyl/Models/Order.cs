using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppVinyl.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPaid { get; set; }
        public string Comments { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

    }
}