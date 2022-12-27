using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private BO.OrderTracking _orderTracking;
        private BLApi.IBl? _bl;
        public OrderTracking(BO.OrderTracking orderTracking, BLApi.IBl bl)
        {
            InitializeComponent();
            DataContext = orderTracking;
            _bl = bl;
            _orderTracking = orderTracking;
            DateAndStatusList.ItemsSource = orderTracking.DateAndStatus;
            

        }

        private void OrderDetails(object sender, RoutedEventArgs e) {
            bool view = true;
            new Order(_bl, _orderTracking.OrderID, view).Show();
        }
    }
}
