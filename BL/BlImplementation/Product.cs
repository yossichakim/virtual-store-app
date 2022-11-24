namespace BlImplementation;

internal class Product : BLApi.IProduct
{
    private DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    /// Returns a list of products - for manager and customer
    /// </summary>
    /// <returns> IEnumerable<BO.ProductForList> </returns>
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

    /// <summary>
    /// Returns a product - for a manager
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.NoValidException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
    public BO.Product GetProductManger(int productID)
    {
        if (productID <= 0)
        {
            throw new BO.NoValidException("product id");
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
        catch (DalApi.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    /// <summary>
    /// Returns a product - for a costumer
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BO.NoValidException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
    public BO.ProductItem GetProductCostumer(int productID, BO.Cart cart)
    {
        if (productID <= 0)
        {
            throw new BO.NoValidException("product id");
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
        catch (DalApi.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    public void AddProduct(BO.Product productToAdd)
    {
        if (productToAdd.ProductID <= 0 ||
            productToAdd.ProductID.ToString().Length < 6 ||
            string.IsNullOrWhiteSpace(productToAdd.ProductID.ToString()))
        {
            throw new BO.NoValidException("product id");
        }

        if (string.IsNullOrWhiteSpace(productToAdd.ProductName) ||
            productToAdd.ProductPrice <= 0 ||
            productToAdd.InStock < 0)
        {
            throw new BO.NoValidException("name / price / stock");
        }

        DO.Product product = new()
        {
            ProductID = productToAdd.ProductID,
            Name = productToAdd.ProductName,
            Price = productToAdd.ProductPrice,
            InStock = productToAdd.InStock,
            Category = (DO.Category)productToAdd.Category
        };

        try
        {
            Dal.Product.Add(product);
        }
        catch (DalApi.AddException ex)
        {
            throw new BO.AddException(ex);
        }
    }

    public void RemoveProduct(int productID)
    {
        if (Dal.OrderItem.GetAll().ToList().FindIndex(item => item.ProductID == productID) != -1)
        {
            throw new BO.ErrorDeleteException("product in the order");
        }
        try
        {
            Dal.Product.Delete(productID);
        }
        catch (DalApi.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    public void UpdateProduct(BO.Product productTOUpdate)
    {
        if (productTOUpdate.ProductID <= 0 ||
            productTOUpdate.ProductID.ToString().Length < 6 ||
            string.IsNullOrWhiteSpace(productTOUpdate.ProductID.ToString()))
        {
            throw new BO.NoValidException("product id");
        }

        if (string.IsNullOrWhiteSpace(productTOUpdate.ProductName) ||
            productTOUpdate.ProductPrice <= 0 ||
            productTOUpdate.InStock < 0)
        {
            throw new BO.NoValidException("name / price / stock");
        }

        DO.Product product = new()
        {
            ProductID = productTOUpdate.ProductID,
            Name = productTOUpdate.ProductName,
            Price = productTOUpdate.ProductPrice,
            InStock = productTOUpdate.InStock
        };
        try
        {
            Dal.Product.Update(product);
        }
        catch (DalApi.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }
}