namespace BO;

public class OrderTracking
{
    public int OrderTrackingID { get; set; }
    public OrderStatus Status { get; set; }
    public List<Tuple<DateTime?, OrderStatus>>? DateAndStatus { get; set; }

    public override string ToString() => $"Order Tracking ID: {OrderTrackingID}\n" +
                                         $"Status: {Status}\n" +
                                         $"Date And Status:\n {string.Join('\n', DateAndStatus)}\n";
}