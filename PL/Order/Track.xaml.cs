using PL.ValidInput;
using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class Track : Window
{
   // private BLApi.IBl? s_bl;
    //private BO.Order? _order;
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    public int OrderId { get; set; }

    public Track()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void OrderTracking(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(OrderID.Text) || !int.TryParse(OrderID.Text, out int n1))
        {
            MessageBox.Show("ENTER A VALID ORDER ID NUMBER", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";
            return;
        }

        try
        {
            new OrderTracking(OrderId, _bl).Show();
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";

        } catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";

        }
    }

    private void OrderID_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = ValidInput.ValidInputs.isValidNumber(e.Text);
    }
}