using SeviceFunction;

namespace BlImplementation;

/// <summary>
/// Cart interface implementation class
/// </summary>
internal class Cart : BLApi.ICart
{
    private DalApi.IDal _dal = new Dal.DalList();

    /// <summary>
    /// This function receives a cart entity and a product ID and adds this product to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns>Returns a cart entity with a product added</returns>
    /// <exception cref="BO.NoValidException">If the quantity of the product is incorrect</exception>
    /// <exception cref="BO.NoFoundException">Product not found</exception>
    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        DO.Product product = new();
        try
        {
            product = _dal.Product.Get(productID);

            if (product.InStock <= 0)
            {
                throw new BO.NoValidException("product stock");
            }
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }

        if (cart.ItemsList is null)
        {
            cart.ItemsList = new();
        }
        if (cart.TotalPriceInCart is null)
        {
            cart.TotalPriceInCart = new();

        }

        BO.OrderItem item = new BO.OrderItem();

        if (cart.ItemsList is not null)
            item = cart.ItemsList.Find(elememnt => elememnt.ProductID == productID);


        if (item != null)
        {
            item.Amount++;
            item.TotalPrice += item.ProductPrice;
            cart.TotalPriceInCart += item.ProductPrice;

        }
        else
        {
            cart.ItemsList.Add(new BO.OrderItem()
            {
                ProductID = productID,
                ProductPrice = product.Price,
                Amount = 1,
                TotalPrice = product.Price,
                ProductName = product.Name,
            });

            cart.TotalPriceInCart += product.Price;
        }
        return cart;
    }

    /// <summary>
    /// The function receives a cart entity and validates the received cart for the order
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.NoValidException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
    /// <exception cref="BO.ErrorUpdateCartException"></exception>
    /// <exception cref="BO.AddException"></exception>
    public void ConfirmedOrder(BO.Cart cart)
    {
        if (string.IsNullOrWhiteSpace(cart.CustomerName) ||
            string.IsNullOrWhiteSpace(cart.CustomerEmail) ||
            string.IsNullOrWhiteSpace(cart.CustomerAddress) ||
            !cart.CustomerEmail.IsValidEmail())
        {
            throw new BO.NoValidException("name / email / address");
        }

        if (cart.ItemsList is not null)
        {
            foreach (var item in cart.ItemsList)
            {
                if (item.Amount <= 0)
                {
                    throw new BO.NoValidException("product amount");
                }
                try
                {
                    DO.Product product = _dal.Product.Get(item.ProductID);
                    if (item.Amount > product.InStock)
                    {
                        throw new BO.NoValidException("product stock");
                    }
                }
                catch (DO.NoFoundException ex)
                {
                    throw new BO.NoFoundException(ex);
                }
            }
        }

        DO.Order order = new()
        {
            CustomerAddress = cart.CustomerAddress,
            CustomerEmail = cart.CustomerEmail,
            CustomerName = cart.CustomerName,
            DeliveryDate = null,
            ShipDate = null,
            OrderDate = DateTime.Now,
        };

        try
        {
            if (cart.ItemsList is not null)
            {
                int orderID = _dal.Order.Add(order);
                foreach (var item in cart.ItemsList)
                {
                    _dal.OrderItem.Add(new DO.OrderItem
                    {
                        Amount = item.Amount,
                        Price = item.ProductPrice,
                        ProductID = item.ProductID,
                        OrderID = orderID
                    });
                    DO.Product product = _dal.Product.Get(item.ProductID);
                    product.InStock -= item.Amount;
                    _dal.Product.Update(product);
                }
            }
            else
            {
                throw new BO.ErrorUpdateCartException("is empty");
            }
        }
        catch (DO.AddException ex)
        {
            throw new BO.AddException(ex);
        }
    }

    /// <summary>
    /// This function receives a cart entity and a product ID and quantity to update a product, and updates the quantity in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="newAmount"></param>
    /// <returns>Returns a cart entity with an updated product</returns
    /// <exception cref="BO.NoValidException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
    public BO.Cart UpdateAmount(BO.Cart cart, int productID, int newAmount)
    {

        try
        {
            DO.Product product = _dal.Product.Get(productID);

            if (newAmount < 0)
            {
                throw new BO.NoValidException("product amount");
            }
            BO.OrderItem item;

            if (cart.ItemsList is null ||!cart.ItemsList.Exists(element => element.ProductID == productID))
            {
                throw new BO.NoValidException("product");
            }

            item = cart.ItemsList.First(element => element.ProductID == product.ProductID);

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
                        throw new BO.NoValidException("product stock");
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
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }

        return cart;
    }
}