using BO;

namespace BLApi;

public interface ICart
{
    public Cart AddProductToCart(Cart cart, int productID);
    public Cart UpdateAmount(Cart cart, int productID, int newAmount);
    public void ConfirmedOrder(Cart cart);
}