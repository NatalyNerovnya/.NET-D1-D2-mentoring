namespace ORM.Entities
{
    using System;

    public class Statistic
    {
        public string Country { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime ShippedDate { get; set; }

        public int OrderID { get; set; }

        public int SaleAmount { get; set; }
    }
}
