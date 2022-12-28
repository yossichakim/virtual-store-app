
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

    public static readonly DependencyProperty ListPropOrder = DependencyProperty.Register(nameof(orderForLists), typeof(IEnumerable<BO.OrderForList?>), typeof(OrderList), new PropertyMetadata(null));
    public IEnumerable<BO.OrderForList?> orderForLists { get => (IEnumerable<BO.OrderForList?>)GetValue(ListPropOrder); set => SetValue(ListPropOrder, value); }

    public OrderList()
    {
        InitializeComponent();
        orderForLists = _bl?.Order.GetOrderList()!;
        OrderListview.ItemsSource = orderForLists;
    }

    private void AccessUpdateOrder(object sender, MouseButtonEventArgs e)
    { 
        if(IsMouseCaptureWithin)
             new Order(_bl, ((BO.OrderForList)OrderListview.SelectedItem).OrderID,false, this).Show();
    }
}