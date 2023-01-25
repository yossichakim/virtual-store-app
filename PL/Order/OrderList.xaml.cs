namespace PL.Order;

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Interaction logic for OrderList.xaml
/// </summary>
public partial class OrderList : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    /// <summary>
    /// Link to the order list for display in case the list will be updated
    /// </summary>
    public static readonly DependencyProperty ListPropOrder = DependencyProperty.Register(nameof(orderForList), typeof(IEnumerable<BO.OrderForList?>), typeof(OrderList), new PropertyMetadata(null));

    /// <summary>
    /// An object for a list of orders
    /// </summary>
    public IEnumerable<BO.OrderForList?> orderForList { get => (IEnumerable<BO.OrderForList?>)GetValue(ListPropOrder); set => SetValue(ListPropOrder, value); }

    /// <summary>
    /// constructor
    /// </summary>
    public OrderList()
    {
        InitializeComponent();
        orderForList = _bl!.Order.GetOrderList()!;
    }

    /// <summary>
    /// If an element in the list is updated, the list will be updated
    /// </summary>
    private void updateOrderList()
    {
        orderForList = _bl!.Order.GetOrderList()!;
    }

    /// <summary>
    /// Access for order update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AccessUpdateOrder(object sender, MouseButtonEventArgs e)
    {
        BO.OrderForList orderForList = (BO.OrderForList)OrderListview.SelectedItem;

        if (IsMouseCaptureWithin && orderForList is not null)
            new Order(((BO.OrderForList)OrderListview.SelectedItem).OrderID, false, updateOrderList).Show();
    }

    /// <summary>
    /// Sort the list by column
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortByColmun(object sender, RoutedEventArgs e)
    {
        GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;
        if (gridViewColumnHeader is not null)
        {
            string tag = (gridViewColumnHeader.Tag as string)!;
            OrderListview.Items.SortDescriptions.Clear();
            OrderListview.Items.SortDescriptions.Add(new SortDescription(tag, ListSortDirection.Ascending));
        }
    }

    /// <summary>
    /// Apply clicking only is the mouse on the selected item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductItemListview_MouseMove(object sender, MouseEventArgs e)
    {
        OrderListview.SelectedItem = null;
    }
}