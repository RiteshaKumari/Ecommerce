using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Productoffer
    {
        public string ModelName { get; set; }
        public string PName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal newPrice { get; set; }
        public string PID { get; set; }
        public string CreatedDate { get; set; }
    }
}