using System.Xml.Serialization;
using WPAZV.Interfaces;
using WPAZV.Model;

namespace WPAZV.Helper
{
    public class XmlHelper{
        public static string Serialize(IEmployee employee){
            using (var writer = new StringWriter()){
                var serializer = new XmlSerializer(typeof(Employee));
                serializer.Serialize(writer, employee);
                return writer.ToString();
            }
        }

        public static IEmployee? Deserialize(string xml){
            using (var reader = new StringReader(xml)){
                var serializer = new XmlSerializer(typeof(Employee));
                return serializer.Deserialize(reader) as IEmployee;
            }
        }
    }
}