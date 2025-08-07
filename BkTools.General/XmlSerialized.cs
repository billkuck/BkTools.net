using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BkTools.General
{
    public class XmlSerialized<T> where T : new()
    {
        public static Type[] ExtraTypes { get; set; } = { };
        private static XmlSerializer _serializer;

        static XmlSerialized()
        {
            _serializer = new XmlSerializer(typeof(T), ExtraTypes);
        }

        public static T? LoadFromXmlFile(string inputFileName)
        {
            var file = new StreamReader(inputFileName);
            var serializableObject = default(T);
            try
            {
                serializableObject = (T?)_serializer.Deserialize(file);
            }
            catch (Exception)
            {
                throw;  // convenient for setting breakpoint
            }
            finally
            {
                file.Close();
            }
            return serializableObject;
        }

        public static T? LoadFromXmlString(string xml) 
            => (T?)_serializer.Deserialize(new StringReader(xml));

        public static string GetXml(T serializableObject)
        {
            using (var xmlStream = new StringWriter())
            {
                var xmlWriter = XmlWriter.Create(xmlStream, new XmlWriterSettings() { Encoding = Encoding.UTF8, Indent = true });
                _serializer.Serialize(xmlWriter, serializableObject);
                return xmlStream.ToString();
            }
        }

        public static void SaveXml(string outputFileName, T serializableObject)
        {
            string xml = GetXml(serializableObject);
            File.WriteAllText(outputFileName, xml);
        }
    }
}
