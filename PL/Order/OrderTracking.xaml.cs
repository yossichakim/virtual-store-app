namespace PL.Order;

using System.Windows;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Window
{
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    public static readonly DependencyProperty OrderTrackingDep = DependencyProperty.Register(nameof(OrderTrackingProp), typeof(BO.OrderTracking), typeof(OrderTracking));
    public BO.OrderTracking? OrderTrackingProp { get => (BO.OrderTracking?)GetValue(OrderTrackingDep); set => SetValue(OrderTrackingDep, value); }

    public OrderTracking(int orderId)
    {
        InitializeComponent();
        OrderTrackingProp = _bl!.Order.OrderTrackingManger(orderId)!;
    }

    private void OrderDetails(object sender, RoutedEventArgs e)
    {
        bool view = true;
        new Order(OrderTrackingProp!.OrderID, view).Show();
    }
}