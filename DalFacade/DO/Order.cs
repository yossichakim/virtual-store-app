namespace DO;

/// <summary>
/// Structure for Order
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Name of Customer
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Email of Customer
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// Address of Customer
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// Date of Order
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// Date of Ship the Order
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// Order delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// A function that overrides the existing function and prints the Order details
    /// </summary>
    /// <returns> Prints the order details </returns>
    public override string ToString() => $"Order ID: {OrderID}\n" +
                                         $"Customer Name: {CustomerName}\n" +
                                         $"Customer Email: {CustomerEmail}\n" +
                                         $"Customer Address: {CustomerAddress}\n" +
                                         $"Order Date: {OrderDate}\n" +
                                         $"Ship Date: {ShipDate}\n" +
                                         $"Delivery Date: {DeliveryDate}\n";
}