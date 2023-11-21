namespace Invoices.Utility;

using System.Text;
using System.Xml.Serialization;

public class XmlHelper
{
    public T Deserialize<T>(string inputXml, string rootName)
    {
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

        using var reader = new StringReader(inputXml);

        return (T)serializer.Deserialize(reader)!;
    }

    public IEnumerable<T> DeserializeCollection<T>(string inputXml, string rootName)
    {
        var xmlSerializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootName));

        using var reader = new StringReader(inputXml);

        return (T[])xmlSerializer.Deserialize(reader)!;
    }

    public string Serialize<T>(T obj, string rootName)
    {
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);

        var sb = new StringBuilder();
        using var writer = new StringWriter(sb);
        serializer.Serialize(writer, obj, namespaces);

        return sb.ToString().TrimEnd();
    }

    public string SerializeCollection<T>(T[] obj, string rootName)
    {
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);

        var sb = new StringBuilder();
        using var writer = new StringWriter(sb);
        serializer.Serialize(writer, obj, namespaces);

        return sb.ToString().TrimEnd();
    }
}