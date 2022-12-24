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

    public NewOrder(string name,string email, string address)
    {
        InitializeComponent();
        _cart = new();

        productItemLists = from item in _bl?.Product.GetProductList()!
                           select _bl?.Product.GetProductCostumer(item.ProductID, _cart)!;
        ProductItemListview.ItemsSource = productItemLists;
        FilterCatgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void FilterCatgory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        => ProductItemListview.ItemsSource = _bl?.Product.Filter(productItemLists,
            item => item?.Categoty == (BO.Category)FilterCatgory.SelectedItem);

    private void ProductItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        bool view = true;

        if(IsMouseCaptureWithin)
        new ProductView(_bl, ((BO.ProductItem)ProductItemListview.SelectedItem).ProductID, view).Show();
    }
}
