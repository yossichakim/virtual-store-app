using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for ClientDetails.xaml
/// </summary>
public partial class ClientDetails : Window
{
    /// <summary>
    /// Object for shopping cart
    /// </summary>
    private BO.Cart? _cart;

    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    /// <summary>
    /// for closing a previous window
    /// </summary>
    private Action _action;

    /// <summary>
    /// For updating the product item list
    /// </summary>
    private Action _productItemChange;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="action"></param>
    /// <param name="productItemChange"></param>
    public ClientDetails(BO.Cart cart, Action action, Action productItemChange)
    {
        InitializeComponent();
        _productItemChange = productItemChange;
        _action = action;
        _cart = cart;
    }

    /// <summary>
    /// for order confirmation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToAddOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            _cart!.CustomerAddress = Address.Text;
            _cart.CustomerName = Name.Text;
            _cart.CustomerEmail = Email.Text;
            s_bl!.Cart.ConfirmedOrder(_cart);
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            _action.Invoke();
            _productItemChange.Invoke();
            this.Close();
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.ErrorUpdateCartException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.AddException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}