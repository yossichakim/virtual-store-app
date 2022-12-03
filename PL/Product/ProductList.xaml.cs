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
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        private IBl _bl = new Bl();
        public ProductList()
        {
            InitializeComponent();
            ProductListview.ItemsSource = _bl.Product.GetProductList();

            FilterProducts.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void FilterProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListview.ItemsSource = _bl.Product.GetProductList()
                .Where(item => item!.Category == (BO.Category)FilterProducts.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        => new Product(_bl).Show();
    }
}
