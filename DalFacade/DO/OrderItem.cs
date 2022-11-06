namespace DO;

/// <summary>
/// Structure for Order Item
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of Order Item
    /// </summary>
    public int OrderItemID { get; set; }
    /// <summary>
    /// Unique ID of Product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// Price of Product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Product quantity in order
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// A function that overrides the existing function and prints the order item details
    /// </summary>
    /// <returns> Prints the order item details </returns>
    public override string ToString() => $@"
        Order Item ID: {OrderItemID}
        Product ID: {ProductID}
        Order ID: {OrderID}
        Price: {Price}
    	Amount in stock: {Amount}";
}