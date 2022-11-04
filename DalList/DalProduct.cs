using DO;

namespace Dal;

public class DalProduct
{
    public int AddProduct(Product addProduct)
    {
        if (Array.Exists(DataSource.products, element => element.ProductID == addProduct.ProductID))
            throw new Exception("the product you try to add already exist");

        if (DataSource.Config._indexProducts == DataSource.products.Length)
            throw new Exception("no more space to add a new product");

        DataSource.products[DataSource.Config._indexProducts++] = addProduct;

        return addProduct.ProductID;
    }

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

    public Product[] GetAllProduct()
    {
        Product[] products = DataSource.products;
        //products = DataSource.products /*new Product[]*/;
        return products;
    }

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
}
