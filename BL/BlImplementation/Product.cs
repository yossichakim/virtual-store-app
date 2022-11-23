namespace BlImplementation;

internal class Product : BLApi.IProduct
{
    private DalApi.IDal Dal = new Dal.DalList();

    public IEnumerable<BO.ProductForList> GetProductList()
    {
        return from item in Dal.Product.GetAll()
               select new BO.ProductForList()
               {
                   ProductID = item.ProductID,
                   ProductName = item.Name,
                   Category = (BO.Category)item.Category,
                   ProductPrice = item.Price
               };
    }
    public BO.Product GetProductManger(int productID)
    {
        if (productID <= 0)
        {
            throw new Exception("product id cannot be a negative or zero");
        }

        try
        {
            DO.Product temp = Dal.Product.Get(productID);

            return new BO.Product()
            {
                ProductPrice = temp.Price,
                Category = (BO.Category)temp.Category,
                ProductName = temp.Name,
                ProductID = productID,
                InStock = temp.InStock
            };
        }
        catch (Exception)
        {

            throw new Exception("product id not found");
        }

    }

    public BO.ProductItem GetProductCostumer(int productID, BO.Cart cart)
    {
        if (productID <= 0)
        {
            throw new Exception("product id cannot be a negative or zero");
        }

        try
        {
            DO.Product temp = Dal.Product.Get(productID);

            int amount = 0;
            foreach (var item in cart.ItemsList)
            {
                if (item.ProductID == productID)
                {
                    amount = item.Amount;
                }
            }
            return new BO.ProductItem()
            {
                ProductID = temp.ProductID,
                ProductName = temp.Name,
                ProductPrice = temp.Price,
                Categoty = (BO.Category)temp.Category,
                InStock = (temp.InStock > 0) ? true : false,
                AmountInCart = amount
            };
        }
        catch (Exception)
        {

            throw new Exception("product id not found");
        }

    }
    public void AddProduct(BO.Product productTOAdd)
    {
        if (productTOAdd.ProductID > 0 && !string.IsNullOrWhiteSpace(productTOAdd.ProductName)
            && productTOAdd.ProductPrice > 0 && productTOAdd.InStock >= 0)
        {
            DO.Product product = new()
            {
                ProductID = productTOAdd.ProductID,
                Name = productTOAdd.ProductName,
                Price = productTOAdd.ProductPrice,
                InStock = productTOAdd.InStock,
            };

            try
            {
                Dal.Product.Add(product);
            }
            catch (Exception)
            {
                throw new Exception("the product id is already exist");
            }
        }
        else
        {
            throw new Exception("one of the details wrong");
        }

    }


    public void RemoveProduct(int productID)
    {
        if (Dal.OrderItem.GetAll().ToList().FindIndex(item => item.ProductID == productID) != -1)
        {
            throw new Exception("IS EXISST IN ORDER");
        }
        try
        {
            Dal.Product.Delete(productID);
        }
        catch
        {
            throw new Exception("the product is not exist");
        }
    }


    public void UpdateProduct(BO.Product productTOUpdate)
    {
        if (productTOUpdate.ProductID > 0 && !string.IsNullOrWhiteSpace(productTOUpdate.ProductName)
          && productTOUpdate.ProductPrice > 0 && productTOUpdate.InStock >= 0)
        {
            DO.Product product = new() {
            ProductID = productTOUpdate.ProductID,
            Name = productTOUpdate.ProductName,
            Price = productTOUpdate.ProductPrice,
            InStock = productTOUpdate.InStock
        };
            try
            {
                Dal.Product.Update(product);
            }
            catch (Exception)
            {
                throw new Exception("the product is not exist");
            }
        }
        else
        {
            throw new Exception("one of the details wrong");
        }
    }

}









