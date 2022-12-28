using System.Windows;
using System.ComponentModel.DataAnnotations;
using System;

namespace PL.Order;

/// <summary>
/// Interaction logic for ClientDetails.xaml
/// </summary>
public partial class ClientDetails : Window
{
    private BO.Cart _cart;

    private BLApi.IBl? _bl;
    private Action action;

    public ClientDetails(BLApi.IBl? bl, BO.Cart cart, Action action)
    {
        InitializeComponent();
        this.action = action;    
        _bl = bl;   
        _cart = cart;   
    }

    private void ToAddOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            _cart.CustomerAddress = Address.Text;
            _cart.CustomerName = Name.Text;
            _cart.CustomerEmail = Email.Text;
            _bl?.Cart.ConfirmedOrder(_cart);
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            //ASK???
            action();
            this.Close();
        } 

        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        } 
        
        catch (BO.NoFoundException ex )
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
