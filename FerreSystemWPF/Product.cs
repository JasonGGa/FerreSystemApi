using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreSystemWPF
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }        
        public double? PurchPriceSol { get; set; }
        public double? PurchPriceDol { get; set; }
        public double? WholesalePrice { get; set; }
        public double? RetailPrice { get; set; }
        public int? Stock { get; set; }
    }
}