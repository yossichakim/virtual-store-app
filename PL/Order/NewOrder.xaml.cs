using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Order;

/// <summary>
/// Interaction logic for NewOrder.xaml
/// </summary>
public partial class NewOrder : Window
{
    private BO.Cart _cart;
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    /// <summary>
    /// Saving the list of products
    /// </summary>
    private IEnumerable<BO.ProductItem?> productItemLists;

    private IEnumerable<IGrouping<BO.Category?, BO.ProductItem>> groupings;
    public NewOrder()
    {
        InitializeComponent();
        _cart = new();
        productItemLists = _bl.Product.GetProductListCostumer(_cart);
        groupings = from item in productItemLists
                    group item by item.Categoty into x
                    select x;
        ProductItemListview.ItemsSource = productItemLists;
        FilterCatgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void FilterCatgory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductItemListview.ItemsSource = groupings.FirstOrDefault(element => element.Key == (BO.Category)FilterCatgory.SelectedItem);
    }

    private void ProductItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

        if(IsMouseCaptureWithin)
        new ProductView(_bl, ((BO.ProductItem)ProductItemListview.SelectedItem).ProductID, _cart).Show();
    }

    private void ShowCart(object sender, RoutedEventArgs e) => new Cart.Cart(_cart).Show();
}
