using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FerreSystemWPF
{
    class HttpServices
    {

        public static IEnumerable<Product> GetAllProductsAsync(HttpClient client)
        {
            IEnumerable<Product> product = null;
            HttpResponseMessage response = client.GetAsync("api/ferre").Result;
            if (response.IsSuccessStatusCode)
            {
                product = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            return product;
        }

        public static Product GetProductAsync(HttpClient client, int id)
        {
            Product product = null;
            HttpResponseMessage response = client.GetAsync($"api/ferre/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                product = response.Content.ReadAsAsync<Product>().Result;
            }
            return product;
        }

        public static Uri CreateProductAsync(HttpClient client, Product product)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("api/ferre", product).Result;
            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location;
        }

        public static Product UpdateProductAsync(HttpClient client, Product product)
        {
            HttpResponseMessage response = client.PutAsJsonAsync($"api/ferre/{product.ProductId}", product).Result;
            response.EnsureSuccessStatusCode();
            
            product = response.Content.ReadAsAsync<Product>().Result;
            return product;
        }

        public static HttpStatusCode DeleteProductAsync(HttpClient client, int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/ferre/{id}").Result;
            return response.StatusCode;
        }
    }
}
