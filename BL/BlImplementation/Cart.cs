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
                throw new Exception("not in stock");
            }
        }
        catch (Exception)
        {
            throw new Exception("error");
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
            string.IsNullOrWhiteSpace(cart.CustomerAddress))
        {
            throw new Exception("one of the values are empty");
        }

        if (!cart.CustomerEmail.IsValidEmail())
        {
            throw new Exception("the email address are not valid");
        }

        foreach (var item in cart.ItemsList)
        {
            if (item.Amount <= 0)
            {
                throw new Exception("not EROREr");
            }
            try
            {
                DO.Product product = Dal.Product.Get(item.ProductID);
                if (item.Amount > product.InStock)
                {
                    throw new Exception("not enough in stock");
                }
            }
            catch (Exception)
            {
                throw new Exception("error");
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

    public BO.Cart UpdateAmount(BO.Cart cart, int productID, int newAmount)
    {
        //לבדוק כמות חדשה חיובית
        BO.OrderItem orderItem = cart.ItemsList.First(orderItem => orderItem.ProductID == productID);

            if (orderItem is not null)
            {
                if (orderItem.Amount > newAmount)
                {
                    //לבדוק האם יש מספיק להוסיף
                }
                else if (orderItem.Amount < newAmount)
                {
                    //להחסיר מהכמות
                    //לבדוק בנוסף אם הכמות השתנתנה ל0
                }

            }
            //לעדכן כמות של מוצר בדאל

        throw new NotImplementedException();
    }
}