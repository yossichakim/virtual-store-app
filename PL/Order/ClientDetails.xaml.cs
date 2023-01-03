using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for ClientDetails.xaml
/// </summary>
public partial class ClientDetails : Window
{
    private BO.Cart? _cart;

    private static BLApi.IBl? s_bl = BLApi.Factory.Get();
    private Action _action;
    private Action _productItemChange;

    public ClientDetails(BO.Cart cart, Action action, Action productItemChange)
    {
        InitializeComponent();
        _productItemChange = productItemChange;
        _action = action;
        _cart = cart;
    }

    private void ToAddOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            _cart!.CustomerAddress = Address.Text;
            _cart.CustomerName = Name.Text;
            _cart.CustomerEmail = Email.Text;
            s_bl!.Cart.ConfirmedOrder(_cart);
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            _action();
            _productItemChange();
            this.Close();
        } catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        } catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        } catch (BO.ErrorUpdateCartException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        } catch (BO.AddException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}