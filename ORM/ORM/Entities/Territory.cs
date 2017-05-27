namespace ORM.Entities
{
    using System.Collections.Generic;

    public class Territory
    {
        public string TerritoryID { get; set; }

        public string TerritoryDescription { get; set; }

        public int RegionID { get; set; }

        public List<Employee> Employees { get; set; }


        public Region Region { get; set; }
    }
}
