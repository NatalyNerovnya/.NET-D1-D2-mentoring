namespace ORM.Mappers
{
    using Dapper.FluentMap.Mapping;

    using ORM.Entities;
    public class EmployeeMap:EntityMap<Employee>
    {
        public EmployeeMap()
        {
            this.Map(e => e.Id).ToColumn("EmployeeID");
        }
    }
}
