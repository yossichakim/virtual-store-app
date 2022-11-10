using DO;

namespace Dal;
/// <summary>
/// class for menage product
/// </summary>
public class DalProduct
{
    /// <summary>
    /// The function receives a new product and adds it
    /// </summary>
    /// <param name="addProduct"></param>
    /// <returns> ID number of the added product </returns>
    /// <exception cref="Exception"> 1. if the product id already exist, 2. if the array of products are full </exception>
    public int AddProduct(Product addProduct)
    {
        if (Array.Exists(DataSource.products, element => element.ProductID == addProduct.ProductID))
            throw new Exception("the product you try to add already exist");

        if (DataSource.indexProducts == DataSource.products.Length)
            throw new Exception("no more space to add a new product");

        DataSource.products[DataSource.indexProducts++] = addProduct;

        return addProduct.ProductID;
    }

    /// <summary>
    /// Gets an ID number of the product and returns the product object
    /// </summary>
    /// <param name="productID"></param>
    /// <returns> returns the product object </returns>
    /// <exception cref="Exception"> if the product not exist </exception>
    public Product GetProduct(int productID)
    {
        if (!Array.Exists(DataSource.products, element => element.ProductID == productID))
            throw new Exception("the product you try to get are not exist");

        Product returnProdcut = new Product();
        foreach (var tmpProduct in DataSource.products)
        {
            if (tmpProduct.ProductID == productID)
            {
                returnProdcut = tmpProduct;
            }
        }
        return returnProdcut;
    }

    /// <summary>
    /// <returns> Returns the list of all products </returns>
    /// </summary>
    public Product[] GetAllProduct()
    {
        //int i = 0;
        Product[] returnProducts = new Product[DataSource.indexProducts];

        //copy all the cells to the new array
        //DataSource.products.CopyTo(returnProducts, 25, 0);
        //foreach (var item in DataSource.products.Select())
        //{
        //    returnProducts[i++] = item;
        //}
        for (int i = 0; i < returnProducts.Length; i++)
        {
            returnProducts[i] = DataSource.products[i];
        }
        return returnProducts.Select(product => product).ToArray();
    }

    /// <summary>
    /// Deletes the product whose ID number was received as a parameter
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception"> if the product not exist </exception>
    public void RemoveProduct(int productId)
    {
        if (!Array.Exists(DataSource.products, element => element.ProductID == productId))
            throw new Exception("the product you try to delete are not exist");

        for (int i = 0; i < DataSource.indexProducts; i++)
        {
            if (DataSource.products[i].ProductID == productId)
            {
                DataSource.products[i] = DataSource.products[--DataSource.indexProducts];
                return;
            }
        }
    }

    /// <summary>
    /// Update product details by the received object
    /// </summary>
    /// <param name="updateProduct"></param>
    /// <exception cref="Exception"> if the product not exist </exception>
    public void UpdateProduct(Product updateProduct)
    {
        if (!Array.Exists(DataSource.products, element => element.ProductID == updateProduct.ProductID))
            throw new Exception("the product you try to update are not exist");

        for (int i = 0; i < DataSource.indexProducts; i++)
        {
            if (updateProduct.ProductID == DataSource.products[i].ProductID)
            {
                DataSource.products[i] = updateProduct;
                return;
            }
        }
    }
}