namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddres { get; set; }
    public OrderItem? ItemsList { get; set; }
    public double TotalPriceInCart { get; set; }
}