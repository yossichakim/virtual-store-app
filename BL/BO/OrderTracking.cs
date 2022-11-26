namespace BO;

/// <summary>
/// order tracking
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// order tracking ID
    /// </summary>
    public int OrderTrackingID { get; set; }

    /// <summary>
    /// status
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// date and status
    /// </summary>
    public List<Tuple<DateTime?, OrderStatus>>? DateAndStatus { get; set; }

    /// <summary>
    /// print order tracking
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Order Tracking ID: {OrderTrackingID}\n" +
                                         $"Status: {Status}\n" +
                                         $"Date And Status:\n {string.Join('\n', DateAndStatus)}\n";
}