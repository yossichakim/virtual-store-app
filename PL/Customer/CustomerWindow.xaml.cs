using PL.Order;
using System.Windows;

namespace PL.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void NewOrder(object sender, RoutedEventArgs e) => new NewOrder().Show();

        private void OrderTracking(object sender, RoutedEventArgs e) => new Track().Show();
    }
}