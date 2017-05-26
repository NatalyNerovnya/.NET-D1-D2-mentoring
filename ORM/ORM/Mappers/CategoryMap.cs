namespace ORM.Mappers
{
    using Dapper.FluentMap.Mapping;

    using ORM.Entities;
    public class CategoryMap: EntityMap<Category>
    {
        public CategoryMap()
        {
            this.Map(c => c.Id).ToColumn("CategoryID");
        }
    }
}
