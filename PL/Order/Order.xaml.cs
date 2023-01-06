namespace PL.Order;

using System.Windows;

/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    /// <summary>
    /// Rear window link to display for order update
    /// </summary>
    public static readonly DependencyProperty OrderDep = DependencyProperty.Register(nameof(OrderProp), typeof(BO.Order), typeof(Order));

    /// <summary>
    /// object for an order
    /// </summary>
    public BO.Order? OrderProp { get => (BO.Order?)GetValue(OrderDep); set => SetValue(OrderDep, value); }

    /// <summary>
    /// For updating the previous window of the order list
    /// </summary>
    private event Action? _orderChanged;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="OrderID"></param>
    /// <param name="view"></param>
    /// <param name="orderChanged"></param>
    public Order(int OrderID, bool view = false, Action? orderChanged = null)
    {
        InitializeComponent();
        _orderChanged = orderChanged;
        OrderProp = _bl!.Order.GetOrderDetails(OrderID)!;
        ItemsListView.ItemsSource = OrderProp.ItemsList;

        if (OrderProp.ShipDate is not null)
            UpdateShip.Visibility = Visibility.Hidden;
        else
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (OrderProp.DeliveryDate is not null)
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (view == true)
        {
            UpdateShip.Visibility = Visibility.Hidden;
            UpdateDelivery.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Update sending an order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        OrderProp = _bl!.Order.ShippingUpdate(OrderProp!.OrderID)!;
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        _orderChanged!.Invoke();
        UpdateShip.Visibility = Visibility.Hidden;
        UpdateDelivery.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Order delivery update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        OrderProp = _bl!.Order.DeliveryUpdate(OrderProp!.OrderID)!;
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        _orderChanged!.Invoke();
        UpdateDelivery.Visibility = Visibility.Hidden;
    }
}