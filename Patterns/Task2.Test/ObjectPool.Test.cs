namespace Task2.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjectPool
    {
        [TestMethod]
        public void ObjectPoolTest()
        {
            var pool = new ObjectPool<PoolObject>(new PoolObjectCreator());

            var elem1 = pool.GetObject();
            var elem2 = pool.GetObject();

            pool.ReturnObject(ref elem1);

            var elem3 = pool.GetObject();

            pool.ReturnObject(ref elem3);
            pool.ReturnObject(ref elem2);

            Assert.AreEqual(2, pool.Count);
        }
    }
}
