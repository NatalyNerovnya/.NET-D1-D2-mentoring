namespace CachingSolutionsSamples.Employees
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    using NorthwindLibrary;

    using StackExchange.Redis;

    public class EmployeesRedisCache : IEmployeeCache
    {
        private ConnectionMultiplexer redisConnection;
        private string prefix = "Cache_Employees";
        private DataContractSerializer serializer = new DataContractSerializer(
            typeof(IEnumerable<Employee>));

        public EmployeesRedisCache(string hostName)
        {
            redisConnection = ConnectionMultiplexer.Connect(hostName);
        }
        public IEnumerable<Employee> Get(string forUser)
        {
            var db = redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<Employee>)serializer
                .ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<Employee> employees)
        {
            var db = redisConnection.GetDatabase();
            var key = prefix + forUser;

            if (employees == null)
            {
                db.StringSet(key, RedisValue.Null, TimeSpan.FromHours(12));
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, employees);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
