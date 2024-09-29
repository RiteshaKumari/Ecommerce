using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class AddCategory
    {
        [Required(ErrorMessage = "required.")]
        public string BusinessCategory { get; set; }
        [Required(ErrorMessage = "required.")]
        public string SellingCategory { get; set; }
        [Required(ErrorMessage = "required.")]
        public string SubCategory { get; set; }
    }
}