using System;
using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class Track : Window
{
   // private BLApi.IBl? _bl;
    //private BO.Order? _order;
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    private BO.OrderTracking? _orderTracking;
    public Track()
    {
        InitializeComponent();
    }

    private void OrderTracking(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(OrderID.Text) || !int.TryParse(OrderID.Text, out int n1))
        {
            MessageBox.Show("ENTER A VALID ORDER ID NUMBER", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";
            return;
        }

        try
        {
            _orderTracking = _bl?.Order.OrderTrackingManger(int.Parse(OrderID.Text));
            new OrderTracking(_orderTracking, _bl).Show();
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";

        } catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";

        }
    }
}