namespace ORM.Entities
{
    using System.Collections.Generic;

    public class Shipper
    {
        public int ShipperID { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }

        public List<Order> Orders { get; set; }
    }
}
