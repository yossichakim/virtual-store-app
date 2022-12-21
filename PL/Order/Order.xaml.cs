using BO;
using System.Windows;
namespace PL.Order;


/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    private BLApi.IBl? _bl;
    public Order(BLApi.IBl? bl, int OrderID)
    {
        InitializeComponent();
        _bl = bl;
        BO.Order order = _bl?.Order.GetOrderDetails(OrderID)!;
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
        Id.IsEnabled = false;
        Name.IsEnabled = false; 
        Email.IsEnabled = false;    
        Address.IsEnabled = false;  
        Status.IsEnabled = false;   
        OrderDate.IsEnabled = false;    
        ShipDate.IsEnabled = false;
        DeliveryDate.IsEnabled = false; 
        TotalPrice.IsEnabled = false;   
    }
}
