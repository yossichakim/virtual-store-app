using BLApi;
using DalApi;
using Dal;
namespace BlImplementation;

internal class Product : BLApi.IProduct
{
    private IDal Dal = new DalList();

    public IEnumerable<BO.ProductForList> GetProductList()
    {

        List<BO.ProductForList> productForLists = new();

        for (int i = 0; i < Dal.Product.GetAll().Count(); i++)
        {
            DO.Product temp = Dal.Product.GetAll().ElementAt(i);
            productForLists[i].ProductID = temp.ProductID;
            productForLists[i].ProductName = temp.Name;
            productForLists[i].ProductPrice = temp.Price;
            productForLists[i].Category = (BO.Category)temp.Category;
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
        retProduct.Category = (BO.Category)temp.Category;
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
    public void AddProduct(BO.Product productTOAdd) 
    {
        if (productTOAdd.ProductID > 0 && !string.IsNullOrWhiteSpace(productTOAdd.ProductName) 
            && productTOAdd.ProductPrice > 0 && productTOAdd.InStock >= 0)
        {
            DO.Product product = new();
            product.ProductID = productTOAdd.ProductID;
            product.Name = productTOAdd.ProductName;    
            product.Price = productTOAdd.ProductPrice;
            product.InStock = productTOAdd.InStock; 
            Dal.Product.Add(product);
            return;
        }
        else { throw new Exception("no valid"); }

    }



    public void RemoveProduct(int productID) 
    {
        for (int i = 0; i < Dal.OrderItem.GetAll().Count(); i++)
        {
            if (Dal.OrderItem.GetAll().ElementAt(i).ProductID == productID)
            {
                throw new Exception("IS EXISST IN ORDER");
            }
        }

        Dal.Product.Delete(productID);

    }


    public void UpdateProduct(BO.Product productTOUpdate)
    {
        if (productTOUpdate.ProductID > 0 && !string.IsNullOrWhiteSpace(productTOUpdate.ProductName)
          && productTOUpdate.ProductPrice > 0 && productTOUpdate.InStock >= 0)
        {
            DO.Product product = new();
            product.ProductID = productTOUpdate.ProductID;
            product.Name = productTOUpdate.ProductName;
            product.Price = productTOUpdate.ProductPrice;
            product.InStock = productTOUpdate.InStock;
            Dal.Product.Update(product);
        }
        else
        {
            throw new Exception("no valid");
        }
    }
}


