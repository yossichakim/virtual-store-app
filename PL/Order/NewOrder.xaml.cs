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
    private IEnumerable<IGrouping<BO.Category?, BO.ProductItem>> groupings;

    public static readonly DependencyProperty ListPropProductItem = DependencyProperty.Register(nameof(productItemLists), typeof(IEnumerable<BO.ProductItem?>), typeof(NewOrder), new PropertyMetadata(null));
    public IEnumerable<BO.ProductItem?> productItemLists { get => (IEnumerable<BO.ProductItem?>)GetValue(ListPropProductItem); set => SetValue(ListPropProductItem, value); }

    public NewOrder()
    {
        InitializeComponent();
        _cart = new();
        productItemLists = _bl.Product.GetProductListCostumer(_cart);
        groupings = from item in productItemLists
                    group item by item.Categoty into x
                    select x;
        FilterCatgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void FilterCatgory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        productItemLists = groupings.FirstOrDefault(element => element.Key == (BO.Category)FilterCatgory.SelectedItem)!;
    }

    private void ProductItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

        if(IsMouseCaptureWithin)
         new ProductView(_bl, ((BO.ProductItem)ProductItemListview.SelectedItem).ProductID, _cart,this).Show();
    }

    private void ShowCart(object sender, RoutedEventArgs e) => new Cart.Cart(_cart,this).Show();
}
