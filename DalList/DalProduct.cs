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
            throw new AddException("PRODUCT");

        DataSource.products.Add(addProduct);

        return addProduct.ProductID;
    }

    /// <summary>
    /// Deletes the product whose ID number was received as a parameter
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public void Delete(int productId)
    {
        if (!DataSource.products.Exists(element => element?.ProductID == productId))
            throw new NoFoundException("PRODUCT");

        DataSource.products.RemoveAll(element => element?.ProductID == productId);
    }

    /// <summary>
    /// Update product details by the received object
    /// </summary>
    /// <param name="updateProduct"></param>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public void Update(Product updateProduct)
    {
        Delete(updateProduct.ProductID);
        Add(updateProduct);
    }

    /// <summary>
    /// Gets an ID number of the product and returns the product object
    /// </summary>
    /// <param name="productID"></param>
    /// <returns> returns the product object </returns>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public Product Get(int productID)
    {
        return Get(product => product?.ProductID == productID);
    }

    /// <summary>
    /// The function receives an condition of an product
    /// and checks whether there is a matching product and returns the product
    /// </summary>
    /// <param name="func"></param>
    /// <returns> returns the product object </returns>
    /// <exception cref="NoFoundException"></exception>
    public Product Get(Func<Product?, bool>? func)
    {
        if (DataSource.products.FirstOrDefault(func!) is Product product)
            return product;

        throw new NoFoundException("PRODUCT");
    }

    /// <summary>
    /// <returns> Returns the list of all products in condition </returns>
    /// </summary>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
        => func is null ? DataSource.products.Select(item => item) :
            DataSource.products.Where(func);
}