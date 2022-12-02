using DalApi;
using DO;

namespace Dal;

/// <summary>
/// class for menage product
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// The function receives a new product and adds it
    /// </summary>
    /// <param name="addProduct"></param>
    /// <returns> ID number of the added product </returns>
    /// <exception cref="AddException"> if the product id already exist </exception>
    public int Add(Product addProduct)
    {
        if (DataSource.products.Exists(element => element?.ProductID == addProduct.ProductID))
            throw new AddException("product");

        DataSource.products.Add(addProduct);

        return addProduct.ProductID;
    }

    /// <summary>
    /// Gets an ID number of the product and returns the product object
    /// </summary>
    /// <param name="productID"></param>
    /// <returns> returns the product object </returns>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public Product Get(int productID)
    {
        return Get(product => product!.Value.ProductID == productID);
    }

    /// <summary>
    /// <returns> Returns the list of all products </returns>
    /// </summary>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
        => func is null ? DataSource.products.Select(item => item) :
            DataSource.products.Where(func);

    /// <summary>
    /// Deletes the product whose ID number was received as a parameter
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public void Delete(int productId)
    {
        if (!DataSource.products.Exists(element => element?.ProductID == productId))
            throw new NoFoundException("product Id");

        DataSource.products.RemoveAll(element => element?.ProductID == productId);
    }

    /// <summary>
    /// Update product details by the received object
    /// </summary>
    /// <param name="updateProduct"></param>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public void Update(Product updateProduct)
    {
        if (!DataSource.products.Exists(element => element?.ProductID == updateProduct.ProductID))
            throw new NoFoundException("Product");

        for (int i = 0; i < DataSource.products.Count(); i++)
        {
            if (updateProduct.ProductID == DataSource.products[i]?.ProductID)
            {
                DataSource.products[i] = updateProduct;
                return;
            }
        }
    }

    public Product Get(Func<Product?, bool>? func)
    {
        if (DataSource.products.FirstOrDefault(func!) is Product product)
        {
            return product;
        }
        throw new NoFoundException("product");
    }
}