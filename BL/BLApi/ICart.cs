using BO;
namespace BLApi;

/// <summary>
/// Cart entity interface
/// </summary>
public interface ICart
{
    /// <summary>
    /// This function receives a cart entity and a product ID and adds this product to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns>Returns a cart entity with a product added</returns>
    public Cart AddProductToCart(Cart cart, int productID);

    /// <summary>
    /// This function receives a cart entity and a product ID and quantity to update a product, and updates the quantity in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="newAmount"></param>
    /// <returns>Returns a cart entity with an updated product</returns>
    public Cart UpdateAmount(Cart cart, int productID, int newAmount);

    /// <summary>
    /// The function receives a cart entity and validates the received cart for the order
    /// </summary>
    /// <param name="cart"></param>
    public void ConfirmedOrder(Cart cart);
}