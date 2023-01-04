namespace PL.Order;
using System.Windows;



/// <summary>
/// Interaction logic for StatisticsOrders.xaml
/// </summary>
public partial class StatisticsOrders : Window
{
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

   
    public StatisticsOrders()
    {
        InitializeComponent();
        StatisticsOrdersByYear.ItemsSource = s_bl!.Order.StatisticsOrdersByYearGroupBy();
    }
}
