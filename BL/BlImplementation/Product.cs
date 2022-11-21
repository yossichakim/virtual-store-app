using BLApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Product : IProduct
{
    private IDal Dal = new DalList();

    public IEnumerable<ProductForList> GetProductList()
    {

        List<ProductForList> productForLists = new();

        for (int i = 0; i < Dal.Product.GetAll().Count(); i++)
        {
            DO.Product temp = Dal.Product.GetAll().ElementAt(i);
            productForLists[i].ProductID = temp.ProductID;
            productForLists[i].ProductName = temp.Name;
            productForLists[i].ProductPrice = temp.Price;
            productForLists[i].Category = (Category)temp.Category;
        }

        return productForLists;
    }
    public BO.Product GetProductManger(int productID)
    {
        if (productID <= 0)
        {
            throw new Exception("product id not found");
        }
        DO.Product temp = Dal.Product.Get(productID);
        BO.Product retProduct = new();
        retProduct.ProductID = temp.ProductID;
        retProduct.ProductName = temp.Name;
        retProduct.ProductPrice = temp.Price;
        retProduct.Category = (Category)temp.Category;
        retProduct.InStock = temp.InStock;
        return retProduct;
    }

    public BO.ProductItem GetProductCostumer(int productID,BO.Cart cart)
    {
        if (productID <= 0)
        {
            throw new Exception("product id not found");
        }
        DO.Product temp = Dal.Product.Get(productID);
        BO.ProductItem retProduct = new();
        retProduct.ProductID = temp.ProductID;
        retProduct.ProductName = temp.Name;
        retProduct.ProductPrice = temp.Price;

        foreach (var item in cart.ItemsList)
        {
            if (item.ProductID == productID)
            {
                retProduct.AmountInCart += item.Amount;
            }
        }

        retProduct.InStock= (temp.InStock > 0)?true:false;
        return retProduct;

    }
    public void AddProduct(Product productTOAdd) { }
    public void RemoveProduct(int productID) { }
    public void UpdateProduct(Product productTOUpdate) { }
}


