namespace Task2
{
    using System.Collections.Concurrent;

    public class ObjectPool<T> where T : class
    {
        private readonly ConcurrentBag<T> container = new ConcurrentBag<T>();

        private readonly IPoolObjectCreator<T> poolObjectCreator;

        public int Count { get { return this.container.Count; } }

        public ObjectPool(IPoolObjectCreator<T> poolObject)
        {
            this.poolObjectCreator = poolObject;
        }

        public T GetObject()
        {
            T obj;
            if (this.container.TryTake(out obj))
            {
                return obj;
            }

            return this.poolObjectCreator.Create();
        }

        public void ReturnObject(ref T obj)
        {
            this.container.Add(obj);
            obj = null;
        }
    }
}
