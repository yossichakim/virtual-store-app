namespace BO;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public Category Categoty { get; set; }
    public int InStock { get; set; }
}