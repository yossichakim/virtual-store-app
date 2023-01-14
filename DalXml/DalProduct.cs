namespace Dal;
using DalApi;
using DO;
using System;

internal class DalProduct : IProduct
{
    private string productPath = @"Product";

    /// <summary>
    /// The function receives a new product and adds it
    /// </summary>
    /// <param name="addProduct"></param>
    /// <returns> ID number of the added product </returns>
    /// <exception cref="AddException"> if the product id already exist </exception>
    public int Add(Product addProduct)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(productPath);

        if (products.Exists(element => element?.ProductID == addProduct.ProductID))
            throw new AddException("PRODUCT");

        products.Add(addProduct);

        XMLTools.SaveListToXMLSerializer(products, productPath);

        return addProduct.ProductID;
    }

    /// <summary>
    /// Deletes the product whose ID number was received as a parameter
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public void Delete(int productId)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(productPath);

        if (!products.Exists(element => element?.ProductID == productId))
            throw new NoFoundException("PRODUCT");

        products.RemoveAll(element => element?.ProductID == productId);

        XMLTools.SaveListToXMLSerializer(products, productPath);

    }

    /// <summary>
    /// Update product details by the received object
    /// </summary>
    /// <param name="updateProduct"></param>
    /// <exception cref="NoFoundException"> if the product not exist </exception>
    public void Update(Product updateProduct)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(productPath);

        Delete(updateProduct.ProductID);
        products.Add(updateProduct);

        XMLTools.SaveListToXMLSerializer(products, productPath);

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
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(productPath);

        if (products.FirstOrDefault(func!) is Product product)
            return product;

        throw new NoFoundException("PRODUCT");
    }

    /// <summary>
    /// <returns> Returns the list of all products in condition </returns>
    /// </summary>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(productPath);

        if (func is null)
            return products.Select(item => item);

        return products.Where(func);
    }
}
