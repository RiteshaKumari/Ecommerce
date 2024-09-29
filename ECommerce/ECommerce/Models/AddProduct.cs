using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class AddProduct
    {
        [Required(ErrorMessage = "Name is required.")]
        public string ProductName { get; set; }
         public string Description { get; set; }
        //public string ProductDescription { get; set; }
        //[Required(ErrorMessage = "Price is required.")]
        //[Range(0, Double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        //public int Price { get; set; }
    }
}