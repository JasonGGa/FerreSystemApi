using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FerreSystemWPF
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {

        public Inventory()
        {
            InitializeComponent();
            RunAsync();
        }

        private void RunAsync()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:50236");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var list = HttpServices.GetAllProductsAsync(client);
                foreach(var item in list)
                {
                    Trace.WriteLine(item.Name);
                }
                ProductsList.ItemsSource = list;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50236");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            
            try
            {
                HttpResponseMessage response = client.GetAsync("/api/ferre").Result;
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                    ProductsList.ItemsSource = products;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Product not Found");
            }
            */
        }        

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddProducts_Click(object sender, RoutedEventArgs e)
        {
            AddProducts aproducts = new AddProducts();
            aproducts.ShowDialog();
        }
    }
}
