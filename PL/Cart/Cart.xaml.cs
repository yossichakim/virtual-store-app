namespace PL.Cart;

using PL.Order;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Window
{
    /// <summary>
    /// s_bl field for use in logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    /// <summary>
    /// DependencyProperty field handler with change
    /// </summary>
    public static readonly DependencyProperty CartDep = DependencyProperty.Register(nameof(CartProp), typeof(BO.Cart), typeof(Cart));

    /// <summary>
    /// get and set property for Cart
    /// </summary>
    public BO.Cart? CartProp { get => (BO.Cart?)GetValue(CartDep); set => SetValue(CartDep, value); }

    /// <summary>
    /// create Action event
    /// </summary>
    private event Action _productItemChange;

    /// <summary>
    /// constructor for Cart Window
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productItemChange"></param>
    public Cart(BO.Cart cart, Action productItemChange)
    {
        InitializeComponent();
        _productItemChange = productItemChange;
        CartProp = cart;
    }

    /// <summary>
    /// function to open ProductItem Window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ItemsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (IsMouseCaptureWithin && (BO.OrderItem)ItemsList.SelectedItem is not null)
        {
            new ProductItem(CartProp!, ((BO.OrderItem)ItemsList.SelectedItem).ProductID, _productItemChange, false).Show();
            this.Close();
        }
    }

    /// <summary>
    /// function to open ClientDetails Window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckOut_Click(object sender, RoutedEventArgs e)
    {
        new ClientDetails(CartProp!, () => Close(), _productItemChange).Show();
    }

    /// <summary>
    /// Apply clicking only is the mouse on the selected item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductListview_MouseMove(object sender, MouseEventArgs e)
    {
        ItemsList.SelectedItem = null;
    }
}