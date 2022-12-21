using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    private BLApi.IBl? _bl;

    private BO.Order order;

    public Order(BLApi.IBl? bl, int OrderID)
    {
        InitializeComponent();
        _bl = bl;
        order = _bl?.Order.GetOrderDetails(OrderID)!;
        Id.Text = OrderID.ToString();
        Name.Text = order.CustomerName;
        Email.Text = order.CustomerEmail;
        Address.Text = order.CustomerAddress;
        Status.Text = order.Status.ToString();
        OrderDate.Text = order.OrderDate.ToString();
        ShipDate.Text = order.ShipDate.ToString();
        DeliveryDate.Text = order.DeliveryDate.ToString();
        TotalPrice.Text = order.TotalPrice.ToString();
        ItemsListView.ItemsSource = order.ItemsList;

        if (order.ShipDate is not null)
            UpdateShip.Visibility = Visibility.Hidden;
        else
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (order.DeliveryDate is not null)
            UpdateDelivery.Visibility = Visibility.Hidden;
    }

    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        order = _bl?.Order.ShippingUpdate(order.OrderID)!;
        ShipDate.Text = order.ShipDate.ToString();
        Status.Text = order.Status.ToString();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        UpdateShip.Visibility = Visibility.Hidden;
        UpdateDelivery.Visibility = Visibility.Visible;
    }

    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        order = _bl?.Order.DeliveryUpdate(order.OrderID)!;
        DeliveryDate.Text = order.DeliveryDate.ToString();
        Status.Text = order.Status.ToString();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        UpdateDelivery.Visibility = Visibility.Hidden;
    }
}