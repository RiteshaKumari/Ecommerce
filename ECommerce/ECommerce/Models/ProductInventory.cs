using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class ProductInventory
    {
        public string ModelName { get; set; }
        public string PName { get; set; }
        public int Quantity { get; set; }
        public string Dimension { get; set; }
        public string SubCategory { get; set; }
        public string PID { get; set; }
        public string PIID { get; set; }
        public string CreatedDate { get; set; }
    }
}