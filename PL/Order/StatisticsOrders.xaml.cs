namespace PL.Order;
using System.Windows;



/// <summary>
/// Interaction logic for StatisticsOrders.xaml
/// </summary>
public partial class StatisticsOrders : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

   /// <summary>
   /// constructor
   /// </summary>
    public StatisticsOrders()
    {
        InitializeComponent();
        StatisticsOrdersByYear.ItemsSource = s_bl!.Order.StatisticsOrdersByYearGroupBy();
    }
}
