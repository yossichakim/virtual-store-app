using System.Windows;
using System.ComponentModel.DataAnnotations;
namespace PL.Order;

/// <summary>
/// Interaction logic for ClientDetails.xaml
/// </summary>
public partial class ClientDetails : Window
{
    private BO.Cart _cart;

    private BLApi.IBl? _bl;

    public ClientDetails(BLApi.IBl? bl, BO.Cart cart)
    {
        InitializeComponent();
        _bl = bl;   
        _cart = cart;   
    }

    private void ToAddOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            _bl?.Cart.ConfirmedOrder(_cart);
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            //ASK???
            this.Close(); // Application.Current.Shutdown();
        }
        catch (System.Exception)
        {

            throw;
        }
        //if (string.IsNullOrWhiteSpace(Name.Text) ||
        //   string.IsNullOrWhiteSpace(Email.Text) ||
        //   string.IsNullOrWhiteSpace(Address.Text) ||
        //   !new EmailAddressAttribute().IsValid(Email.Text))
        //{
        //    MessageBox.Show("the Name\\Email\\Address is not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    return;
        //}
        //new NewOrder(Name.Text, Email.Text, Address.Text).Show();
    }
       
}
