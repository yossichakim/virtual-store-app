namespace PL.Order;
using System.Windows;


/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    public static readonly DependencyProperty OrderDep = DependencyProperty.Register(nameof(OrderProp), typeof(BO.Order), typeof(Order));
    public BO.Order? OrderProp { get => (BO.Order?)GetValue(OrderDep); set => SetValue(OrderDep, value); }

    private event Action? _orderChanged;
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

    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        OrderProp = _bl!.Order.ShippingUpdate(OrderProp!.OrderID)!;
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        _orderChanged!.Invoke();
        UpdateShip.Visibility = Visibility.Hidden;
        UpdateDelivery.Visibility = Visibility.Visible;
    }

    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        OrderProp = _bl!.Order.DeliveryUpdate(OrderProp!.OrderID)!;
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        _orderChanged!.Invoke();
        UpdateDelivery.Visibility = Visibility.Hidden;
    }
}