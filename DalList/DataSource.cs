using DO;

namespace Dal;
internal static class DataSource {
    static DataSource() { }
    public static List<Product> Products { get; set; }  = new List<Product>(50);

}
