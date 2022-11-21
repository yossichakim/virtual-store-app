using BO;

namespace BLApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetProductList();
    public BO.Product GetProductManger(int productID);
    public BO.ProductItem GetProductCostumer(int productID, Cart cart);
    public void AddProduct(Product productTOAdd);
    public void RemoveProduct(int productID);
    public void UpdateProduct(Product productTOUpdate);
}