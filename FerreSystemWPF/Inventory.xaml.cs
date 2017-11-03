using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        HttpClient client;

        IEnumerable<Product> _products = null;
        public IEnumerable<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        public double exrate = Convert.ToDouble(File.ReadAllText("exrate.txt"));

        public Inventory()
        {
            InitializeComponent();
            DataContext = this;
            RunAsync();
            ExchangeRate.Text = exrate + "";
            Update.IsEnabled = false;
        }

        private void RunAsync()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:50236");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Products = HttpServices.GetAllProductsAsync(client);
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            exrate = Convert.ToDouble(ExchangeRate.Text);
            foreach (var item in Products)
            {
                if (item.PurchPriceDol != null)
                {
                    item.PurchPriceSol = item.PurchPriceDol * exrate;
                    try
                    {
                        HttpServices.UpdateProductAsync(client, item);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
            }
            ProductsList.ItemsSource = null;
            ProductsList.ItemsSource = Products;
            File.WriteAllText("exrate.txt", exrate + "");
            MessageBox.Show("Lista Actualizada");
        }
    }
}
