using PL.Order;
using System.Windows;

namespace PL.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        /// <summary>
        /// constructor
        /// </summary>
        public CustomerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Access for creating a new order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewOrder(object sender, RoutedEventArgs e) => new NewOrder().Show();

        /// <summary>
        /// Access for order tracking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderTracking(object sender, RoutedEventArgs e) => new Track().Show();
    }
}