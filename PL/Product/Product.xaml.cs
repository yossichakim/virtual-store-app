using BLApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        private IBl _bl;

        public Product(IBl bl)
        {
            InitializeComponent();
            _bl = bl;
            Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product()
            {
                ProductID = int.Parse(Id.Text),
                ProductName = Name.Text,
                Category = (BO.Category)Catgory.SelectedItem,
                ProductPrice = double.Parse(Price.Text),
                InStock = int.Parse(Instock.Text)
            };
            try
            {
                _bl.Product.AddProduct(product);
                MessageBox.Show("seucsse");
                this.Close();
            }
            catch 
            {
                MessageBox.Show("error");
            }
        }
    }
}
