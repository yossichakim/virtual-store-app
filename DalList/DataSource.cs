using DO;

namespace Dal;
public static class DataSource {
    static DataSource() { }
    public static List<Product> Products { get; set; }  = new List<Product>(50);
}
