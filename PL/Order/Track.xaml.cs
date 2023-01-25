namespace PL.Order;

using System.Windows;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class Track : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    /// <summary>
    /// constructor
    /// </summary>
    public Track()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Go to the order tracking window by entering the order number
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// for input only numbers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private new void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = ValidInput.ValidInputs.isValidNumber(e.Text);
    }
}