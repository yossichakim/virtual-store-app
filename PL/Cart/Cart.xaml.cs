namespace PL.Cart;

using PL.Order;
using System.Windows;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Window
{
    private static BLApi.IBl? _bl = BLApi.Factory.Get();

    public static readonly DependencyProperty CartDep = DependencyProperty.Register(nameof(CartProp), typeof(BO.Cart), typeof(Cart));
    public BO.Cart? CartProp { get => (BO.Cart?)GetValue(CartDep); set => SetValue(CartDep, value); }

    private event Action _productItemChange;

    public Cart(BO.Cart cart, Action productItemChange)
    {
        InitializeComponent();
        _productItemChange = productItemChange;
        CartProp = cart;
    }

    private void ItemsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (IsMouseCaptureWithin)
            new ProductItem(CartProp!, ((BO.OrderItem)ItemsList.SelectedItem).ProductID, _productItemChange, false).Show();
        this.Close();
    }

    private void CheckOut_Click(object sender, RoutedEventArgs e)
    { 
        new ClientDetails(CartProp!, () => Close(), _productItemChange).Show(); 
    }
}