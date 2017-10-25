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

        public static async Task<Product> GetProductAsync(HttpClient client, int id)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync($"api/ferre/{id}");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        public static async Task<Uri> CreateProductAsync(HttpClient client, Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/ferre", product);
            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location;
        }

        public static async Task<Product> UpdateProductAsync(HttpClient client, Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/ferre/{product.ProductId}", product);
            response.EnsureSuccessStatusCode();
            
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        public static async Task<HttpStatusCode> DeleteProductAsync(HttpClient client, int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/ferre/{id}");
            return response.StatusCode;
        }
    }
}
