namespace Task2.Test
{
    using System;

    public class PoolObject
    {
        public PoolObject()
        {
            Console.WriteLine("New pool object");
        }
    }

    public class PoolObjectCreator : IPoolObjectCreator<PoolObject>
    {
        public PoolObject Create()
        {
            return new PoolObject();
        }
    }
}
