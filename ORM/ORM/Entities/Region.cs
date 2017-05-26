namespace ORM.Entities
{
    using System.Collections.Generic;

    public class Region
    {
        public int Id { get; set; }

        public string RegionDescription { get; set; }

        public List<Territory> Territories { get; set; }
    }
}
