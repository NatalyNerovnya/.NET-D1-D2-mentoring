namespace ORM.Mappers
{
    using Dapper.FluentMap.Mapping;
    using ORM.Entities;

    public class SupplierMap : EntityMap<Supplier>
    {
        public SupplierMap()
        {
            this.Map(p => p.Id).ToColumn("SupplierID");
        }
    }
}
