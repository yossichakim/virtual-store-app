using BO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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

    private void AccessUpdateOrder(object sender, MouseButtonEventArgs e)
    => new Order(_bl, ((BO.OrderForList)OrderListview.SelectedItem).OrderID).Show();
}