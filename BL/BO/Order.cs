namespace BO;

/// <summary>
/// Order
/// </summary>
public class Order
{
    /// <summary>
    /// ID number for the order
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// customer email
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// customer address
    /// </summary>
    public string CustomerAddress { get; set; }

    /// <summary>
    /// order date
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// ship date
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// items list in order
    /// </summary>
    public List<OrderItem>? ItemsList { get; set; }

    /// <summary>
    /// total price for order
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// print order
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Customer Name: {CustomerName}\n" +
                                         $"Customer Email: {CustomerEmail}\n" +
                                         $"Customer Address: {CustomerAddress}\n" +
                                         $"Order Date: {OrderDate}\n" +
                                         $"Status: {Status}\n" +
                                         $"Ship Date: {ShipDate}\n" +
                                         $"Delivery Date: {DeliveryDate}\n" +
                                         $"Items List: {string.Join('\n', ItemsList)}\n" +
                                         $"Total Price order: {TotalPrice}\n";
}