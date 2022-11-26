using BO;

namespace BLApi;

/// <summary>
/// Product entity interface
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Returns a list of all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> GetProductList();

    /// <summary>
    /// Gets a product ID and returns a product entity - for a manager
    /// </summary>
    /// <param name="productID"></param>
    /// <returns>returns a product entity</returns>
    public Product GetProductManger(int productID);

    /// <summary>
    /// Receives a product ID and a cart entity and returns a product entity - for a customer
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="cart"></param>
    /// <returns>returns a product entity</returns>
    public ProductItem GetProductCostumer(int productID, Cart cart);

    /// <summary>
    /// Gets a product entity and adds to the list of products
    /// </summary>
    /// <param name="productTOAdd"></param>
    public void AddProduct(Product productTOAdd);

    /// <summary>
    /// Gets a product and delivery entity from the list of products
    /// </summary>
    /// <param name="productID"></param>
    public void RemoveProduct(int productID);

    /// <summary>
    /// Gets a product entity and updates the list of products
    /// </summary>
    /// <param name="productTOUpdate"></param>
    public void UpdateProduct(Product productTOUpdate);
}