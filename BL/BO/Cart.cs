namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<OrderItem>? ItemsList { get; set; }
    public double TotalPriceInCart { get; set; }

    public override string ToString() => $"Customer Name: {CustomerName}\n" +
                                         $"Customer Email: {CustomerEmail}\n" +
                                         $"Customer Address: {CustomerAddress}\n" +
                                         $"Items List: {ItemsList}\n" +
                                         $"Total Price In Cart: {TotalPriceInCart}\n";
}