using BO;

namespace BLApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetProductList();
    public Product GetProductManger(int productID);
    public Product GetProductCostumer(int productID);
    public void AddProduct(Product productTOAdd);
    public void RemoveProduct(int productID);
    public void UpdateProduct(Product productTOUpdate);
}