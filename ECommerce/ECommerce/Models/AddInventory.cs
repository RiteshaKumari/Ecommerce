using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class AddInventory
    {
        public string ModelName { get; set; }
        
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, Double.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        //[Required(ErrorMessage = "Price is required.")]
        //[Range(0, Double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        //public int Price { get; set; }

        [Required(ErrorMessage = "Dimension must be specified.")]
        public int Width { get; set; }
        public int Height { get; set; }

        public string Dimension
        {
            get { return $"{Width}*{Height}"; }
            set
            {
                var dimensions = value.Split('*');
                Width = int.Parse(dimensions[0]);
                Height = int.Parse(dimensions[1]);
            }
        }
        [Required]
        public string StockUnit { get; set; }
        [Required]
        public string LiquidContent { get; set; }
        [Required]
        public string AssemblyRequired { get; set; }
        [Required]
        public string ManufacturedMaterial { get; set; }

        //[Required(ErrorMessage = "It is required.")]
        [Range(0, Double.MaxValue, ErrorMessage = "It must be a non-negative number.")]
        public string NumberofSetPerItem { get; set; }
    }
}