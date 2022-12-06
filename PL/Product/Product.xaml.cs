using BLApi;
using System;
using System.Windows;
namespace PL.Product;

/// <summary>
/// Interaction logic for Product.xaml
/// </summary>
public partial class Product : Window
{
    private IBl _bl;

    public Product(IBl bl)
    {
        InitializeComponent();
        _bl = bl;
        Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        BO.Product product = new BO.Product()
        {
            ProductID = int.Parse(Id.Text),
            ProductName = Name.Text,
            Category = (BO.Category)Catgory.SelectedItem,
            ProductPrice = double.Parse(Price.Text),
            InStock = int.Parse(Instock.Text)
        };

        try
        {
            _bl.Product.AddProduct(product);
            MessageBox.Show("הפעולה בוצעה בהצלחה");
            this.Close();
        }
        catch (BO.AddException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message);
        }
    }
}