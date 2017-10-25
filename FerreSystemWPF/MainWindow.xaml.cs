using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerreSystemWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TicketItem> _ticketItemList = new ObservableCollection<TicketItem>();

        public ObservableCollection<TicketItem> TicketItemList
        {
            get { return _ticketItemList; }
            set { _ticketItemList = value; }
        }

        TicketItem _ttprice = new TicketItem();

        public TicketItem TicketTotalP
        {
            get { return _ttprice; }
            set { _ttprice = value; }
        }

        IEnumerable<Product> _source = null;
        public IEnumerable<Product> Source
        {
            get { return _source; }
            set { _source = value; }
        }

        Product _theSelectedItem = null;
        public Product TheSelectedItem
        {
            get { return _theSelectedItem; }
            set { _theSelectedItem = value; }
        }

        Product product = null;

        public MainWindow()
        {            
            InitializeComponent();
            DataContext = this;
            RunAsync();
            File.WriteAllText("exrate.txt","3.25");
        }

        public void RunAsync()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:50236")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //var list = HttpServices.GetAllProductsAsync(client);
                //ProductList.ItemsSource = list;
                Source = HttpServices.GetAllProductsAsync(client);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product = TheSelectedItem;
            if (product != null)
            {
                if (product.PurchPriceDol != null)
                {
                    double exrate = Convert.ToDouble(File.ReadAllText("exrate.txt"));
                    PrecioAprox.Text = string.Format("{0:#0.00}", product.PurchPriceDol * exrate);
                }
                else
                    PrecioAprox.Text = string.Format("{0:#0.00}", product.PurchPriceSol);
                Costo.Text = PrecioAprox.Text;

                PrecioPub.Text = string.Format("{0:#0.00}", product.RetailPrice);
                PrecioMayor.Text = string.Format("{0:#0.00}", product.WholesalePrice);
                Stock.Text = product.Stock + "";

                Trans.IsChecked = false;
                Mayor.IsChecked = false;
                Menor.IsChecked = false;
                Factu.IsChecked = false;
            }
            else
            {
                Clear();
            }
        }

        private void Clear()
        {
            PrecioAprox.Text = "";
            PrecioPub.Text = "";
            PrecioMayor.Text = "";
            Stock.Text = "";

            Trans.IsChecked = false;
            Mayor.IsChecked = false;
            Menor.IsChecked = false;
            Factu.IsChecked = false;

            Cantidad.Text = "1";
            product = null;
            ProductList.SelectedIndex = -1;
            Costo.Text = "";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            String checkBox = (sender as CheckBox).Name;

            if (product != null)
            {
                double sum = Convert.ToDouble(Costo.Text);

                if (checkBox == "Trans")
                {
                    sum += Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.02, 2);
                }
                else if (checkBox == "Mayor")
                {
                    sum += Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.05, 2);
                }
                else if (checkBox == "Menor")
                {
                    sum += Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.10, 2);
                }
                else
                {
                    sum += Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.03, 2);
                }

                Costo.Text = string.Format("{0:#0.00}", sum);
            }
            else
                (sender as CheckBox).IsChecked = false;            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            String checkBox = (sender as CheckBox).Name;

            if (product != null)
            {
                double res = Convert.ToDouble(Costo.Text);

                if (checkBox == "Trans")
                {
                    res -= Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.02, 2);
                }
                else if (checkBox == "Mayor")
                {
                    res -= Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.05, 2);
                }
                else if (checkBox == "Menor")
                {
                    res -= Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.10, 2);
                }
                else
                {
                    res -= Math.Round(Convert.ToDouble(product.PurchPriceSol) * 0.03, 2);
                }

                Costo.Text = string.Format("{0:#0.00}", res);
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
            {
                int n = TicketItemList.Count + 1;
                int qua = Convert.ToInt32(Cantidad.Text);
                Product item = product;
                double price = Convert.ToDouble(Costo.Text);
                double tprice = Math.Round(price * qua, 2);

                TicketItemList.Add(new TicketItem { Nitem = n, Quantity = qua, Item = item, UnitPrice = price, TotalPrice = tprice });
                Clear();
                TicketTotalP.TotalPrice += tprice;
            }
        }

        private void DelItem_Click(object sender, RoutedEventArgs e)
        {
            TicketItem p = TicketItems.SelectedItem as TicketItem;
            _ticketItemList.Remove(p);
        }

        private void TicketItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DelItem.IsEnabled = true;
        }
    }
}