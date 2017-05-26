namespace ORM.Entities
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public List<Product> Products { get; set; }
    }
}
