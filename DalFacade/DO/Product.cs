using System.ComponentModel;

namespace DO;
public struct Product {
    public int ProductID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }
}