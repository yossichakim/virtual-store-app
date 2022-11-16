namespace BO;

public class OrderTracking
{
    public int ID  { get; set; }
    public OrderStatus Name { get; set; }
    public List<Tuple<OrderStatus, DateTime>>? DateAndStatus { get; set; }
    public override string ToString() => $"ID: {ID}\n" +
                                         $"Name: {Name}\n" +
                                         $"Date And Status: {DateAndStatus}\n";
}