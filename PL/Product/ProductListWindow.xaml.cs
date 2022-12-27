using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
public partial class ProductList : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    /// <summary>
    /// Saving the list of products
    /// </summary>
    public static readonly DependencyProperty ListProp = DependencyProperty.Register(nameof(productFor), typeof(IEnumerable<BO.ProductForList?>), typeof(ProductList), new PropertyMetadata(null));
    public IEnumerable<BO.ProductForList?> productFor { get => (IEnumerable<BO.ProductForList?>)GetValue(ListProp); set => SetValue(ListProp, value); }

    /// <summary>
    /// constructor for product list
    /// </summary>
    public ProductList()
    {
        InitializeComponent();
        productFor = _bl?.Product.GetProductList()!;
      //  ProductListview.ItemsSource = productForL;
        FilterProducts.ItemsSource = Enum.GetValues(typeof(BO.Category));

    }

    /// <summary>
    /// Filter the list of products by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FilterProductsClick(object sender, SelectionChangedEventArgs e) =>
        ProductListview.ItemsSource = _bl?.Product.Filter(productFor,
            item => item!.Category == (BO.Category)FilterProducts.SelectedItem);

    /// <summary>
    /// Access for adding a product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AccessAddProduct(object sender, RoutedEventArgs e)
    => new ProductView(_bl, this).Show();

    /// <summary>
    /// Refreshing the list of products and presenting without filtering
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AllCatgoryClick(object sender, RoutedEventArgs e)
    => ProductListview.ItemsSource = _bl?.Product.GetProductList();

    /// <summary>
    /// Access for product update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AccessUpdateProduct(object sender, MouseButtonEventArgs e)
    {
           if (IsMouseCaptureWithin)
                new ProductView(_bl, ((BO.ProductForList)ProductListview.SelectedItem).ProductID, this ).Show();

    }
}