using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerreSystemApi.Models;

namespace FerreSystemApi.Models
{
    public class DbInitializer
    {
        public static void Initialize(FerreContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product{Name="Codo 2 x 2 Desagüe", PurchPriceSol=0.82, WholesalePrice=0.9, RetailPrice=1.0, Stock=40},
                new Product{Name="Codo 4 x 2 Desagüe", PurchPriceSol=3.62, WholesalePrice=3.95, RetailPrice=4.2, Stock=10},
                new Product{Name="Codo 4 x 4 Desagüe", PurchPriceSol=3.9, WholesalePrice=4.2, RetailPrice=4.8, Stock=20}
            };
            
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();            
        }
    }
}
