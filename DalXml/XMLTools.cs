namespace Dal;

using System.Xml.Linq;
using System.Xml.Serialization;

static class XMLTools
{
    const string s_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T?>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(stream, list);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while serializing the list: " + ex.Message);
        }
    }
    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            XmlSerializer serializer = new XmlSerializer(typeof(List<T?>));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return serializer.Deserialize(stream) as List<T?> ?? new();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deserializing the file: " + ex.Message);
        }
    }

    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }

    }

    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);

            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
            
        }
        catch (Exception ex)
        {
            //new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file:{ filePath} ", ex);
            throw new Exception($"fail to load xml file: {filePath} ", ex);
        }
    }
}