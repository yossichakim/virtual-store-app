namespace BO;

/// <summary>
/// Order helper entity
/// </summary>
public class OrderForList
{
    /// <summary>
    ///order ID
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// status of order
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// amount of items
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// total price
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// print
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"OrderID: {OrderID}\n" +
                                         $"Customer Name: {CustomerName}\n" +
                                         $"Status: {Status}\n" +
                                         $"Amount Of Items: {AmountOfItems}\n" +
                                         $"Total Price: {TotalPrice}\n";
}