namespace BO;

/// <summary>
///order item
/// </summary>
public class OrderItem
{
    /// <summary>
    /// product ID
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// product name
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// product price
    /// </summary>
    public double ProductPrice { get; set; }

    /// <summary>
    /// amount
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// total price
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// print order item
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"ProductName: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Amount: {Amount}\n" +
                                         $"Total Price: {TotalPrice}\n";
}