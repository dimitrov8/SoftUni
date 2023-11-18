namespace CarDealer.Utilities;

using System.Text;
using System.Xml.Serialization;

public class XmlHelper
{
    public T Deserialize<T>(string inputXml, string rootName)
    {
        var xmlRoot = new XmlRootAttribute(rootName);
        var xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

        using var reader = new StringReader(inputXml);
        var dtos = (T)xmlSerializer.Deserialize(reader)!;

        return dtos;
    }

    public IEnumerable<T> DeserializeCollection<T>(string inputXml, string rootName)
    {
        var xmlRoot = new XmlRootAttribute(rootName);
        var xmlSerializer = new XmlSerializer(typeof(T[]), xmlRoot);

        using var reader = new StringReader(inputXml);
        var dtos = (T[])xmlSerializer.Deserialize(reader)!;

        return dtos;
    }

    public string Serialize<T>(T obj, string rootName)
    {
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

        var xmlNamespaces = new XmlSerializerNamespaces();
        xmlNamespaces.Add(string.Empty, string.Empty);

        var sb = new StringBuilder();
        using var writer = new StringWriter(sb);
        serializer.Serialize(writer, obj, xmlNamespaces);

        return sb.ToString().TrimEnd();
    }

    public string Serialize<T>(T[] obj, string rootName)
    {
        var serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootName));

        var xmlNamespaces = new XmlSerializerNamespaces();
        xmlNamespaces.Add(string.Empty, string.Empty);

        var sb = new StringBuilder();
        using var writer = new StringWriter(sb);
        serializer.Serialize(writer, obj, xmlNamespaces);

        return sb.ToString().TrimEnd();
    }
}