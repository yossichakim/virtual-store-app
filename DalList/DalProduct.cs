using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// The function receives a new product and adds it if there is still room
    /// </summary>
    /// <param name="addProduct"></param>
    /// <returns> ID number of the added product </returns>
    /// <exception cref="Exception"></exception>
    public int AddProduct(Product addProduct)
    {
        if (Array.Exists(DataSource.products, element => element.ProductID == addProduct.ProductID))
            throw new Exception("the product you try to add already exist");

        if (DataSource.Config._indexProducts == DataSource.products.Length)
            throw new Exception("no more space to add a new product");

        DataSource.products[DataSource.Config._indexProducts++] = addProduct;

        return addProduct.ProductID;
    }

    /// <summary>
    /// Gets an ID number of the product and returns the product object
    /// </summary>
    /// <param name="_productId"></param>
    /// <returns> returns the product object </returns>
    /// <exception cref="Exception"></exception>
    public Product GetProduct(int _productId)
    {
        if (!Array.Exists(DataSource.products, element => element.ProductID == _productId))
            throw new Exception("the product you try to get/remove are not exist");

        Product returnProdcut = new Product();
        foreach (var tmpProduct in DataSource.products)
        {
            if (tmpProduct.ProductID == _productId)
            {
                returnProdcut = tmpProduct;
            }
        }
        return returnProdcut;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns> Returns the list of products </returns>
    public Product[] GetAllProduct()
    {

        Product[] products = new Product[DataSource.Config._indexProducts];

        DataSource.products.CopyTo(products, 0);

        return products;
    }

    /// <summary>
    /// Deletes the product whose ID number was received as a parameter
    /// </summary>
    /// <param name="_productId"></param>
    /// <exception cref="Exception"></exception>
    public void RemoveProduct(int _productId)
    {
        if (!Array.Exists(DataSource.products, element => element.ProductID == _productId))
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
    /// <param name="updateProduct"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateProduct(Product updateProduct)
    {
        if (!Array.Exists(DataSource.products, element => element.ProductID == updateProduct.ProductID))
            throw new Exception("the product you try to update are not exist");


        for (int i = 0; i < DataSource.Config._indexProducts; i++)
        {
            if (updateProduct.ProductID == DataSource.products[i].ProductID)
            {
                DataSource.products[i] = updateProduct;
                return;
            }
        }
    }
}
