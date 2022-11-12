namespace DO;

/// <summary>
/// Structure for Product
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID of Product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Name of Product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Price of Product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Category of Product
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// Product quantity in stock
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// A function that overrides the existing function and prints the product details
    /// </summary>
    /// <returns> Prints the product details </returns>
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {Name}\n" +
                                         $"Category: {Category}\n" +
                                         $"Price: {Price}\n" +
    	                                 $"Amount in stock: {InStock}\n";
}