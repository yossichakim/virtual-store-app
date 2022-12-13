namespace DalApi;
using DO;
using System.Xml.Linq;

/// <summary>
/// Loading the data from dalXml
/// </summary>
internal static class DalConfig
{
    /// <summary>
    /// name of dal
    /// </summary>
    internal static string s_dalName;

    /// <summary>
    ///dal Packages DalList or DalXml 
    /// </summary>
    internal static Dictionary<string, string> s_dalPackages;

    /// <summary>
    /// constractor
    /// </summary>
    /// <exception cref="DalConfigException"></exception>
    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}