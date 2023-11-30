namespace Footballers.Utilities;

using System.Text;
using System.Xml.Serialization;

public class XmlHelper
{
    public T Deserialize<T>(string xmlString, string rootName)
    {
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

        using var reader = new StringReader(xmlString);

        return (T)serializer.Deserialize(reader)!;
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
}