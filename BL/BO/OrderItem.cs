namespace BO;

public class OrderItem
{
    public int OrderItemID { get; set; }

    public int ProductID { get; set; }

    public double ProductPrice { get; set; }

    public int Amount { get; set; }

    public double TotalPrice { get; set; }
}