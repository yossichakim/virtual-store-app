using System.Windows;
using System.ComponentModel.DataAnnotations;
namespace PL.Order;

/// <summary>
/// Interaction logic for ClientDetails.xaml
/// </summary>
public partial class ClientDetails : Window
{
    public ClientDetails()
    {
        InitializeComponent();
    }

    private void ToAddOrder(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Name.Text) ||
           string.IsNullOrWhiteSpace(Email.Text) ||
           string.IsNullOrWhiteSpace(Address.Text) ||
           !new EmailAddressAttribute().IsValid(Email.Text))
        {
            MessageBox.Show("the Name\\Email\\Address is not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        new NewOrder(Name.Text, Email.Text, Address.Text).Show();
    }
       
}
