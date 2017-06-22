namespace BasicSerialization
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(TypeName = "catalog")]
    [XmlRoot(Namespace = "http://library.by/catalog")]
    public class Catalog
    {
        [XmlElement("book")]
        public List<Book> Books { get; set; }

        [XmlAttribute("date")]
        public DateTime Date { get; set; }
    }
}
