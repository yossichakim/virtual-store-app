using DO;

namespace BO;

public class Order
{
    public int OrderID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddres { get; set; }
    public DateTime OrderDate { get; set; } 
    public OrderStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryrDate { get; set; } 
    public OrderItem? Items { get; set; }    
    public double TotalPrice { get; set; }

}
