using DO;
using SeviceFunction;

namespace BlImplementation;

internal class Cart : BLApi.ICart
{
    private DalApi.IDal Dal = new Dal.DalList();

    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        DO.Product product = new();

        try
        {
            product = Dal.Product.Get(productID);

            if (product.InStock <= 0)
            {
                throw new BO.NoValidException("product stock");
            }
        }
        catch (NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }

        BO.OrderItem item = cart.ItemsList.Find(elememnt => elememnt.ProductID == productID);
        if (item != null)
        {
            item.Amount++;
            item.TotalPrice += item.ProductPrice;
        }
        else
        {
            cart.ItemsList.Add(new BO.OrderItem()
            {
                ProductID = productID,
                ProductPrice = product.Price,
                Amount = 1,
                TotalPrice = product.Price
            });
        }
        cart.TotalPriceInCart += item.ProductPrice;

        return cart;
    }

    public void ConfirmedOrder(BO.Cart cart)
    {
        if (string.IsNullOrWhiteSpace(cart.CustomerName) &&
            string.IsNullOrWhiteSpace(cart.CustomerEmail) &&
            string.IsNullOrWhiteSpace(cart.CustomerAddress)&&
            !cart.CustomerEmail.IsValidEmail())
        {
            throw new BO.NoValidException("name / email / address");
        }

        foreach (var item in cart.ItemsList)
        {
            if (item.Amount <= 0)
            {
                throw new BO.NoValidException("product amount");
            }
            try
            {
                DO.Product product = Dal.Product.Get(item.ProductID);
                if (item.Amount > product.InStock)
                {
                    throw new BO.NoValidException("product stock");
                }
            }
            catch (NoFoundException ex)
            {
                throw new BO.NoFoundException(ex);
            }
        }

        DO.Order order = new()
        {
            CustomerAddress = cart.CustomerAddress,
            CustomerEmail = cart.CustomerEmail,
            CustomerName = cart.CustomerName,
            DeliveryDate = null,
            ShipDate = null,
            OrderDate = DateTime.Now
        };

        try
        {
            int orderID = Dal.Order.Add(order);

            foreach (var item in cart.ItemsList)
            {
                Dal.OrderItem.Add(new DO.OrderItem
                {
                    Amount = item.Amount,
                    Price = item.ProductPrice,
                    ProductID = item.ProductID,
                    OrderID = orderID
                });
                DO.Product product = Dal.Product.Get(item.ProductID);
                product.InStock -= item.Amount;
                Dal.Product.Update(product);
            }
        }
        catch (AddException ex)
        {
            throw new BO.AddException(ex);
        }
    }

    public BO.Cart UpdateAmount(BO.Cart cart, int productID, int newAmount)
    {
        if (newAmount < 0)
        {
            throw new BO.NoValidException("product amount");
        }
        try
        {
            DO.Product product = Dal.Product.Get(productID);

            BO.OrderItem item = cart.ItemsList.First(item => item.ProductID == productID);
            int difference = 0;

            if (item is not null)
            {
                if (newAmount == 0)
                {
                    cart.TotalPriceInCart -= item.TotalPrice;
                    cart.ItemsList.Remove(item);
                }
                else if (newAmount > item.Amount)
                {
                    if (product.InStock < newAmount)
                    {
                        throw new Exception("error");
                    }
                    difference = newAmount - item.Amount;
                    item.Amount = newAmount;
                    item.TotalPrice = item.ProductPrice * newAmount;
                    cart.TotalPriceInCart += difference * item.ProductPrice;
                }
                else if (newAmount < item.Amount)
                {
                    difference = item.Amount - newAmount;
                    item.Amount = newAmount;
                    item.TotalPrice = item.ProductPrice * newAmount;
                    cart.TotalPriceInCart -= difference * item.ProductPrice;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return cart;
    }
}