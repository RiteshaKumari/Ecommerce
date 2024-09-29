using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class AddSubInventory
    {
        public int piece { get; set; }
        [Required(ErrorMessage = "Price must be specified.")]
        public decimal price { get; set; }
        public string size { get; set; }
        public string capacity { get; set; }
        public string color { get; set; }
        public string PSIID { get; set; }
    }
}