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
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// date and status
    /// </summary>
    public List<Tuple<DateTime?, OrderStatus>?>? DateAndStatus { get; set; }

    /// <summary>
    /// print order tracking
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string str = string.Empty;
        str = $"Order Tracking ID: {OrderTrackingID}\n";
        switch (Status)
        {
            case OrderStatus.OrderConfirmed:
                str += "Status: Order Confirmed\n";
                break;

            case OrderStatus.OrderSent:
                str += "Status: Order Sent\n";
                break;

            case OrderStatus.OrderProvided:
                str += "Status: Order Provided\n";
                break;

            default:
                break;
        }

        str += $"Date And Status:\n" + $"{string.Join('\n', DateAndStatus)}\n";

        return str;
    }
}