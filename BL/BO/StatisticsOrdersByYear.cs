namespace BO;

/// <summary>
///Statistics Orders By Year for manager
/// </summary>
public struct StatisticsOrdersByYear
{
    /// <summary>
    /// year of order
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    ///  orders to year
    /// </summary>
    public int? CountOrderPerYear { get; set; }

    //public double? CountOrderSales { get; set; }
}