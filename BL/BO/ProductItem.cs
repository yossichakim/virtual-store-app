namespace BO;

public class ProductItem
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public Category Categoty { get; set; }
    public int AmountInCart { get; set; }
    public bool InStock { get; set; }
}