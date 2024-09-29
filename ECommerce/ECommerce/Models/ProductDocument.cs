using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class ProductDocument
    {
        [Required(ErrorMessage = "Please Upload Image")]
        public HttpPostedFileBase File { get; set; }

        public int TotalQuantity { get; set; }
        public string BName { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "GST Number must be exactly 15 characters long.")]
        public string GSTNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string ModeOfPayment { get; set; }
        public string CreatedDate { get; set; }
    }
}