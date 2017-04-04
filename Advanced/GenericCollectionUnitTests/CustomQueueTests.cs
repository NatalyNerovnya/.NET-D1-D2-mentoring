using System.Linq;
using GenericCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericCollectionUnitTests
{
    [TestClass]
    public class CustomQueueTests
    {
        [TestMethod]
        public void Enqueue_StringArrayAddString_StringArrayWithString()
        {
            CustomQueue<string> actual = new CustomQueue<string>(new[] { "Queue", "123" });
            CustomQueue<string> expected = new CustomQueue<string>(new[] { "Queue", "123", "Hello, world!" });

            actual.Enqueue("Hello, world!");

            CollectionAssert.AreEquivalent(actual.ToArray(), expected.ToArray());
        }

        [TestMethod]
        public void Enqueue_IntArrayAddInt_IntArrayWith()
        {
            CustomQueue<int> actual = new CustomQueue<int>(new[] { 1111,222,33,4 });
            CustomQueue<int> expected = new CustomQueue<int>(new[] { 1111, 222, 33, 4, 555 });

            actual.Enqueue(555);

            CollectionAssert.AreEquivalent(actual.ToArray(), expected.ToArray());
        }

        [TestMethod]
        public void Dequeue_StringArrayWithFirstString_StringArrayWithoutFirstString()
        {
            CustomQueue<string> actual = new CustomQueue<string>(new []{ "Queue", "123", "Hello, world!"});
            CustomQueue<string> expected = new CustomQueue<string>(new[] { "123", "Hello, world!" });

            var expectedValue = actual.Dequeue();

            CollectionAssert.AreEquivalent(actual.ToArray(), expected.ToArray());
            Assert.AreEqual(expectedValue, "Queue");
        }

        [TestMethod]
        public void Dequeue_IntArrayWithFirstInt_IntArrayWithoutFirstInt()
        {
            CustomQueue<int> actual = new CustomQueue<int>(new[] { 1111, 222, 33, 4, 555, 6, 7, 888, 9 });
            CustomQueue<int> expected = new CustomQueue<int>(new[] { 222, 33, 4, 555, 6, 7, 888, 9 });

            var expectedValue = actual.Dequeue();

            CollectionAssert.AreEquivalent(actual.ToArray(), expected.ToArray());
            Assert.AreEqual(expectedValue, 1111);
        }

        [TestMethod]
        public void Peek_StringArrayWithFirstString_StringArrayWithoutFirstString()
        {
            CustomQueue<string> queue = new CustomQueue<string>(new[] { "Queue", "123", "Hello, world!" });
            string expected = "Queue";

            object result = queue.Peek();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Contain_StringArrayWithSummer_True()
        {
            CustomQueue<string> queue = new CustomQueue<string>(new[] { "Queue", "123", "Summer" });

            bool result = queue.Contain("Summer");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Contain_StringArrayWithoutSummer_False()
        {
            CustomQueue<string> queue = new CustomQueue<string>(new[] { "Queue", "123", "Summmmmer" });

            bool result = queue.Contain("Summer");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void EqualToObject_StringArrayAndInt_False()
        {
            CustomQueue<string> queue = new CustomQueue<string>(new[] { "Queue", "123", "Summmmmer" });

            bool result = queue.EqualToObject(123);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void EqualToObject_StringArrayAndNotExistetdString_False()
        {
            CustomQueue<string> queue = new CustomQueue<string>(new[] { "Queue", "123", "Summmmmer" });

            bool result = queue.EqualToObject("Not exist in array");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void EqualToObject_StringArrayAndExistetdString_True()
        {
            CustomQueue<string> queue = new CustomQueue<string>(new[] { "Queue", "123","Existed string", "Summmmmer" });

            bool result = queue.EqualToObject("Existed string");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EqualToObject_IntArrayAndExistetdInt_True()
        {
            CustomQueue<int> queue = new CustomQueue<int>(new[] { 1111, 222, 33, 4, 555, 6, 7, 888, 9 });

            bool result = queue.EqualToObject(4);

            Assert.AreEqual(true, result);
        }
    }
}
