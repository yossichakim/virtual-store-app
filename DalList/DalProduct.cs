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
    /// <param name="_addProduct"></param>
    /// <returns> ID number of the added product </returns>
    /// <exception cref="Exception"> 1. if the product id already exist, 2. if the array of products are full </exception>
    public int AddProduct(Product _addProduct)
    {
        if (Array.Exists(DataSource.products, _element => _element.ProductID == _addProduct.ProductID))
            throw new Exception("the product you try to add already exist");

        if (DataSource.Config._indexProducts == DataSource.products.Length)
            throw new Exception("no more space to add a new product");

        DataSource.products[DataSource.Config._indexProducts++] = _addProduct;

        return _addProduct.ProductID;
    }

    /// <summary>
    /// Gets an ID number of the product and returns the product object
    /// </summary>
    /// <param name="_productID"></param>
    /// <returns> returns the product object </returns>
    /// <exception cref="Exception"> if the product not exist </exception>
    public Product GetProduct(int _productID)
    {
        if (!Array.Exists(DataSource.products, _element => _element.ProductID == _productID))
            throw new Exception("the product you try to get are not exist");

        Product _returnProdcut = new Product();
        foreach (var _tmpProduct in DataSource.products)
        {
            if (_tmpProduct.ProductID == _productID)
            {
                _returnProdcut = _tmpProduct;
            }
        }
        return _returnProdcut;
    }

    /// <summary>
    /// <returns> Returns the list of all products </returns>
    /// </summary>
    public Product[] GetAllProduct()
    {
        Product[] _products = new Product[DataSource.Config._indexProducts];

        //copy all the cells to the new array
        DataSource.products.CopyTo(_products, 0);

        return _products;
    }

    /// <summary>
    /// Deletes the product whose ID number was received as a parameter
    /// </summary>
    /// <param name="_productId"></param>
    /// <exception cref="Exception"> if the product not exist </exception>
    public void RemoveProduct(int _productId)
    {
        if (!Array.Exists(DataSource.products, _element => _element.ProductID == _productId))
            throw new Exception("the product you try to delete are not exist");

        for (int i = 0; i < DataSource.Config._indexProducts; i++)
        {
            if (DataSource.products[i].ProductID == _productId)
            {
                DataSource.products[i] = DataSource.products[--DataSource.Config._indexProducts];
                return;
            }
        }
    }

    /// <summary>
    /// Update product details by the received object
    /// </summary>
    /// <param name="_updateProduct"></param>
    /// <exception cref="Exception"> if the product not exist </exception>
    public void UpdateProduct(Product _updateProduct)
    {
        if (!Array.Exists(DataSource.products, _element => _element.ProductID == _updateProduct.ProductID))
            throw new Exception("the product you try to update are not exist");

        for (int i = 0; i < DataSource.Config._indexProducts; i++)
        {
            if (_updateProduct.ProductID == DataSource.products[i].ProductID)
            {
                DataSource.products[i] = _updateProduct;
                return;
            }
        }
    }
}