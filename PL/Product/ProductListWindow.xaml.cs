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
        private IEnumerable<BO.ProductForList?> productForLists;
        public ProductList()
        {
            InitializeComponent();
            productForLists = _bl.Product.GetProductList();
            ProductListview.ItemsSource = productForLists;
            FilterProducts.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void FilterProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListview.ItemsSource =
                _bl.Product.Filter(productForLists, item => item!.Category == (BO.Category)FilterProducts.SelectedItem);

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        => new Product(_bl).Show();

        private void AllCatgory_Click(object sender, RoutedEventArgs e)
          =>  ProductListview.ItemsSource = _bl.Product.GetProductList();
 

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        => new Product(_bl , ((BO.ProductForList)ProductListview.SelectedItem).ProductID).Show();
    }
}
