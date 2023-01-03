namespace PL.Order;

using System.Windows;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class Track : Window
{
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    public Track()
    {
        InitializeComponent();
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
            new OrderTracking(int.Parse(OrderID.Text)).Show();
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            OrderID.Text = "";
        }
        catch (BO.NoFoundException ex)
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