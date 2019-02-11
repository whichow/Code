using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class SerializationUtil {

    public static T FromBinary<T>(byte[] bytes)
    {
		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream stream = new MemoryStream(bytes))
		{
			return (T)formatter.Deserialize(stream);
		}
    }

    public static T FromXml<T>(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
		using(MemoryStream stream = new MemoryStream(bytes))
		{
        	return (T)serializer.Deserialize(stream);
		}
    }

    public static T FromJson<T>(byte[] bytes)
    {
        JsonSerializer serializer = new JsonSerializer();
		using(MemoryStream stream = new MemoryStream(bytes))
		{
			using(TextReader reader = new StreamReader(stream))
			{
        		return (T)serializer.Deserialize(reader, typeof(T));
			}
		}
    }

	public static byte[] ToBinary(object data)
    {
		BinaryFormatter formatter = new BinaryFormatter();  
        using(MemoryStream stream = new MemoryStream())
		{
			formatter.Serialize(stream, data);
			return stream.ToArray();
        }
    }

    public static byte[] ToXml(object data)
    {
		XmlSerializer serializer = new XmlSerializer(data.GetType());
		using(MemoryStream stream = new MemoryStream())
		{
			serializer.Serialize(stream, data);
			return stream.ToArray();
		}
    }

    public static byte[] ToJson(object data)
    {
		JsonSerializer serializer = new JsonSerializer();
		using(MemoryStream stream = new MemoryStream())
		{
			using(TextWriter writer = new StreamWriter(stream))
			{
				serializer.Serialize(writer, data);
				writer.Flush();
				return stream.ToArray();
			}
		}
    }
}
