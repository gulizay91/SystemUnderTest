using System.Xml.Serialization;

namespace Sample.Infrastructure
{
    public static class XmlExtensions
    {
        public static T? Deserialize<T>(this string input) where T : class
        {
            var ser = new XmlSerializer(typeof(T));
            using var sr = new StringReader(input);
            return (T)ser.Deserialize(sr)!;
        }

    }
}
