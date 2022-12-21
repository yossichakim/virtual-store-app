using BO;
using System.Collections.Generic;
using System.Windows;
namespace PL.Order;

/// <summary>
/// Interaction logic for OrderList.xaml
/// </summary>
public partial class OrderList : Window
{
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    private IEnumerable<OrderForList?> orderForLists;

    public OrderList()
    {
        InitializeComponent();
        orderForLists = _bl?.Order.GetOrderList()!;
        OrderListview.ItemsSource = orderForLists;

    }
}
