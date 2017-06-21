namespace BasicSerialization.ConsoleTest
{
    using System.IO;
    using System.Xml.Serialization;

    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "../../App_Data/books.xml";
            var serializer = new XmlSerializer(typeof(Catalog));
            var catalog = serializer.Deserialize(
         new FileStream(filePath, FileMode.Open)) as Catalog;

        }
    }
}
