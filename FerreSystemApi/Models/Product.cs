using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerreSystemApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }        
        public double? PurchPriceSol { get; set; }
        public double? PurchPriceDol { get; set; }
        public double? WholesalePrice { get; set; }
        public double? RetailPrice { get; set; }
        public int? Stock { get; set; }
    }
}