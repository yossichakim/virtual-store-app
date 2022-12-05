namespace BlImplementation;

/// <summary>
/// Implementation of product functions
/// </summary>
internal class Product : BLApi.IProduct
{
    /// <summary>
    /// Access to dal
    /// </summary>
    private DalApi.IDal _dal = new Dal.DalList();

    /// <summary>
    /// Returns a list of products - for manager and customer
    /// </summary>
    /// <returns> IEnumerable<BO.ProductForList> </returns>
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        return from item in _dal.Product.GetAll()

               select new BO.ProductForList()
               {
                   ProductID = item?.ProductID,
                   ProductName = item?.Name,
                   Category = (BO.Category)item?.Category!,
                   ProductPrice = item?.Price
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
            DO.Product temp = _dal.Product.Get(productID);

            return new BO.Product()
            {
                ProductPrice = temp.Price,
                Category = (BO.Category)temp.Category!,
                ProductName = temp.Name,
                ProductID = productID,
                InStock = temp.InStock
            };
        }
        catch (DO.NoFoundException ex)
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
            DO.Product temp = _dal.Product.Get(productID);

            int amount = 0;
            if (cart.ItemsList is not null)
            {
                foreach (var item in cart.ItemsList)
                {
                    if (item!.ProductID == productID)
                    {
                        amount = item.Amount;
                    }
                }
            }
            return new BO.ProductItem()
            {
                ProductID = temp.ProductID,
                ProductName = temp.Name,
                ProductPrice = temp.Price,
                Categoty = (BO.Category)temp.Category!,
                InStock = (temp.InStock > 0) ? true : false,
                AmountInCart = amount
            };
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    /// <summary>
    /// add product
    /// </summary>
    /// <param name="productToAdd"></param>
    /// <exception cref="BO.NoValidException"></exception>
    /// <exception cref="BO.AddException"></exception>
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
            Category = (DO.Category)productToAdd.Category!
        };

        try
        {
            _dal.Product.Add(product);
        }
        catch (DO.AddException ex)
        {
            throw new BO.AddException(ex);
        }
    }

    /// <summary>
    /// remove product
    /// </summary>
    /// <param name="productID"></param>
    /// <exception cref="BO.ErrorDeleteException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
    public void RemoveProduct(int productID)
    {
        if (_dal.OrderItem.GetAll().ToList().FindIndex(item => item?.ProductID == productID) != -1)
        {
            throw new BO.ErrorDeleteException("product in the order");
        }
        try
        {
            _dal.Product.Delete(productID);
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    /// <summary>
    /// update product
    /// </summary>
    /// <param name="productTOUpdate"></param>
    /// <exception cref="BO.NoValidException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
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
            _dal.Product.Update(product);
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }
}