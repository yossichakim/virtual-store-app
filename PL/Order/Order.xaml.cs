using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    private BLApi.IBl? _bl;

    private BO.Order order;

    OrderList orderList;
    public Order(BLApi.IBl? bl, int OrderID, bool view = false, OrderList sender = null)
    {
        InitializeComponent();
        _bl = bl;
        orderList = sender;
        order = _bl?.Order.GetOrderDetails(OrderID)!;
        DataContext = order;

        ItemsListView.ItemsSource = order.ItemsList;

        if (order.ShipDate is not null)
            UpdateShip.Visibility = Visibility.Hidden;
        else
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (order.DeliveryDate is not null)
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (view == true)
        {
            UpdateShip.Visibility = Visibility.Hidden;
            UpdateDelivery.Visibility = Visibility.Hidden;
        }
    }

    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        order = _bl?.Order.ShippingUpdate(order.OrderID)!;
        DataContext = order;
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        orderList.orderForLists = _bl?.Order.GetOrderList()!;
        UpdateShip.Visibility = Visibility.Hidden;
        UpdateDelivery.Visibility = Visibility.Visible;
    }

    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        order = _bl?.Order.DeliveryUpdate(order.OrderID)!;
        DataContext = order;
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        orderList.orderForLists = _bl?.Order.GetOrderList()!;
        UpdateDelivery.Visibility = Visibility.Hidden;
    }
}