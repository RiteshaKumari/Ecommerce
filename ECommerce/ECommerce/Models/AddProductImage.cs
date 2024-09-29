using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class AddProductImage
    {
        [Required(ErrorMessage = "Please Upload Image")]
        public HttpPostedFileBase File { get; set; }
      
        public string PIID { get; set; }
        public int ID { get; set; }
        public string PSIID { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
     

    }
}