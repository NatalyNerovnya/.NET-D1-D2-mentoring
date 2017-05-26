namespace ORM.Mappers
{
    using Dapper.FluentMap.Mapping;
    using ORM.Entities;

    public class ProductMap: EntityMap<Product>
    {
        public ProductMap()
        {
            this.Map(p => p.Id).ToColumn("ProductID");
        }
    }
}
