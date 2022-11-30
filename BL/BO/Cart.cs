namespace BO;

/// <summary>
/// A shopping basket entity
/// </summary>
public class Cart
{
    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// customer email
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// customer address
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// The products added to the basket
    /// </summary>
    public List<OrderItem?>? ItemsList { get; set; }

    /// <summary>
    /// The total price of the products in the basket
    /// </summary>
    public double? TotalPriceInCart { get; set; }

    /// <summary>
    /// Printing the basket
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string str = string.Empty;
        if (!string.IsNullOrEmpty(CustomerName))
        {
            str = $"Customer Name: {CustomerName}\n" +
                  $"Customer Email: {CustomerEmail}\n" +
                  $"Customer Address: {CustomerAddress}\n";
        }

        return str +
               $"Items List:\n{string.Join('\n', ItemsList)}\n" +
               $"Total Price In Cart: {TotalPriceInCart}\n";
    }
}