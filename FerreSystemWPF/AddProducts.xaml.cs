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
using System.Windows.Shapes;

namespace FerreSystemWPF
{
    /// <summary>
    /// Interaction logic for Agregar.xaml
    /// </summary>
    public partial class AddProducts : Window
    {
        HttpClient client;
        public double exrate = Convert.ToDouble(File.ReadAllText("exrate.txt"));

        IEnumerable<Product> _products = null;
        public IEnumerable<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        private void RunAsync()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:50236")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public AddProducts()
        {
            InitializeComponent();
            DataContext = this;
            RunAsync();
            AddProductsList.InitializingNewItem += (sender, e) =>
            {
                //NewProduct newCourse = e.NewItem as NewProduct;
            };
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }

        private int errorCount;
        private void OnErrorEvent(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("erroe");
            var validationEventArgs = e as ValidationErrorEventArgs;
            if (validationEventArgs == null)
                throw new Exception("Unexpected event args");
            switch (validationEventArgs.Action)
            {
                case ValidationErrorEventAction.Added:
                    {
                        errorCount++; break;
                    }
                case ValidationErrorEventAction.Removed:
                    {
                        errorCount--; break;
                    }
                default:
                    {
                        throw new Exception("Unknown action");
                    }
            }
            Add.IsEnabled = errorCount == 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AddProductsList.CancelEdit();
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<NewProduct> lproducts = (ObservableCollection<NewProduct>) this.FindResource("products");

            foreach (var item in lproducts)
            {
                Product tmp = new Product();
                tmp.Name = item.Name;
                if (item.PurchPriceDol != null && item.PurchPriceDol != 0)
                {
                    tmp.PurchPriceDol = item.PurchPriceDol;
                }
                else
                    tmp.PurchPriceSol = item.PurchPriceSol;

                tmp.RetailPrice = item.RetailPrice;
                tmp.WholesalePrice = item.WholesalePrice;
                tmp.Stock = item.Quantity;

                HttpServices.CreateProductAsync(client, tmp);
            }
            MessageBox.Show("Productos Agregados correctamente");
            this.Close();
            /*
            List<NewProduct> newProducts = AddProductsList.Items.OfType<NewProduct>().ToList();
            Trace.WriteLine(newProducts[0].Quantity);
            Trace.WriteLine(newProducts[0].Name);
            Trace.WriteLine(newProducts[0].PurchPriceDol);
            Trace.WriteLine(newProducts[0].PurchPriceSol);
            Trace.WriteLine(newProducts[0].WholesalePrice);
            Trace.WriteLine(newProducts[0].RetailPrice);
            */

            /*
            for (int i = 0; i < AddProductsList.Items.Count; i++)
            {
                var row = (DataGridRow)AddProductsList.ItemContainerGenerator.ContainerFromIndex(i);
                Trace.WriteLine(row);
            }*/
        }

        private void AddProductsList_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGridRow selected = e.Row;

            if (Validation.GetHasError(selected))
            {
                Trace.WriteLine("3");
            }
            else
            {
                Trace.WriteLine("4");
            }

            if (e.EditAction == DataGridEditAction.Commit)
            {
                NewProduct np = e.Row.DataContext as NewProduct;
                Trace.WriteLine("1" + np.Name);
            }
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                NewProduct np = e.Row.DataContext as NewProduct;
                Trace.WriteLine("2" + np.Name);
            }
        }
    }
    
    public class NewProducts : ObservableCollection<NewProduct>
    {
        public NewProducts()
        {
        }
    }

    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            NewProduct newProduct = (value as BindingGroup).Items[0] as NewProduct;
            if (newProduct.Quantity == 0)
            {
                MessageBox.Show("Ingrese la cantidad de productos");
                return new ValidationResult(false, "Ingrese la cantidad de productos");
            }
            else if (newProduct.Name == null)
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return new ValidationResult(false, "Ingrese el nombre del producto");
            }
            else if (newProduct.PurchPriceDol == null && newProduct.PurchPriceSol == null)
            {
                MessageBox.Show("Ingrese precio de compra (Soles o Dólares)");
                return new ValidationResult(false, "Ingrese precio de compra (Soles o Dólares)");

            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
