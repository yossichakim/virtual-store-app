using PL.Order;
using PL.Product;
using System.Windows;

namespace PL.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Window
{
    private BO.Cart _cart;
    private BLApi.IBl? _bl = BLApi.Factory.Get();
    private NewOrder _newOrder;
    public Cart(BO.Cart cart, NewOrder newOrder)
    {
        InitializeComponent();
        _newOrder = newOrder;
        _cart = cart;
        ItemsList.ItemsSource = cart.ItemsList;
        DataContext = _cart;
    }

    private void ItemsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (IsMouseCaptureWithin)
            new ProductItem(_bl, ((BO.OrderItem)ItemsList.SelectedItem).ProductID, _cart, "updateCart", _newOrder).Show();
        this.Close();
    }

    private void CheckOut_Click(object sender, RoutedEventArgs e) => new ClientDetails(_bl, _cart, _newOrder, () => Close()).Show();
    
}
