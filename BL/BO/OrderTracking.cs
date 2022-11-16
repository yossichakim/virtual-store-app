namespace BO;

public class OrderTracking
{
    public int ID  { get; set; }
    public OrderStatus Name { get; set; }
    public Tuple<DateTime, OrderStatus> DateAndStatus { get; set; }
}