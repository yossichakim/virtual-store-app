using BLApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        /// <summary>
        /// Access to the logical layer
        /// </summary>
        private IBl _bl = new Bl();

        /// <summary>
        /// Saving the list of products
        /// </summary>
        private IEnumerable<BO.ProductForList?> productForLists;

        /// <summary>
        /// constructor for product list
        /// </summary>
        public ProductList()
        {
            InitializeComponent();
            productForLists = _bl.Product.GetProductList();
            ProductListview.ItemsSource = productForLists;
            FilterProducts.ItemsSource = Enum.GetValues(typeof(BO.Category));
  
        }

        /// <summary>
        /// Filter the list of products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterProductsClick(object sender, SelectionChangedEventArgs e) =>
            ProductListview.ItemsSource = _bl.Product.Filter(productForLists,
                item => item!.Category == (BO.Category)FilterProducts.SelectedItem);

        /// <summary>
        /// Access for adding a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccessAddProduct(object sender, RoutedEventArgs e)
        => new ProductView(_bl).Show();

        /// <summary>
        /// Refreshing the list of products and presenting without filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCatgoryClick(object sender, RoutedEventArgs e)
        => ProductListview.ItemsSource = _bl.Product.GetProductList();

        /// <summary>
        /// Access for product update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccessUpdateProduct(object sender, MouseButtonEventArgs e)
        => new ProductView(_bl, ((BO.ProductForList)ProductListview.SelectedItem).ProductID).Show();
    }
}