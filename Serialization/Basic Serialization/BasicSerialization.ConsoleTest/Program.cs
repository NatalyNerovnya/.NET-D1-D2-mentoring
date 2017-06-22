namespace BasicSerialization.ConsoleTest
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    class Program
    {
        static void Main(string[] args)
        {
            var catalog = new Catalog();
            var filePath = "../../App_Data/";
            var serializer = new XmlSerializer(typeof(Catalog));

            using (var fileStream = new FileStream(filePath + "books.xml", FileMode.Open))
            {
                catalog = serializer.Deserialize(fileStream) as Catalog;
            }

            Console.WriteLine($"catalog, date={catalog.Date}");
            foreach (var book in catalog.Books)
            {
                ShowBook(book);
            }

            using (var stream = new FileStream(filePath + "Serialized Books.xml", FileMode.Create))
            {
                serializer.Serialize(stream, catalog);
                Console.WriteLine("File with serialized object were created in App_Data");
            }


            Console.WriteLine("Rate me with excelent mark if you like this app!");
            Console.ReadLine();
        }


        private static void ShowBook(Book book)
        {
            Console.WriteLine($"--book, id={book.Id}");
            Console.WriteLine($"----isbn={book.Isbn}");
            Console.WriteLine($"----author={book.Author}");
            Console.WriteLine($"----title={book.Title}");
            Console.WriteLine($"----genre={book.Genre}");
            Console.WriteLine($"----publisher={book.Publisher}");
            Console.WriteLine($"----publish_date={book.PublishDate}");
            Console.WriteLine($"----registration_date={book.RegistrationDate}");
            Console.WriteLine($"----description={book.Description}\n");
        }
    }
}
