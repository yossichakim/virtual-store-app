using BLApi;
using BO;
using PL.Cart;
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
        _cart = new BO.Cart { CustomerAddress = address, CustomerEmail = email, CustomerName = name, ItemsList = new() };

        productItemLists = from item in _bl?.Product.GetProductList()!
                           select _bl?.Product.GetProductCostumer(item.ProductID, _cart)!;
        ProductItemListview.ItemsSource = productItemLists;
        FilterCatgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void FilterCatgory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        => ProductItemListview.ItemsSource = _bl?.Product.Filter(productItemLists,
            item => item?.Categoty == (BO.Category)FilterCatgory.SelectedItem);
}
