namespace BO;

public class Cart
{

    public string? CustomerName { get; set; }    
    public string? CustomerEmail { get; set; }
    public string? CustomerAddres { get; set; }  
    public OrderItem? Items { get; set; }    
    public double TotalPrice { get; set; }  

}
