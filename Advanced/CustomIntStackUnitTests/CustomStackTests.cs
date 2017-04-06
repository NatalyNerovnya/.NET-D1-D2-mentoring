using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomIntStack;

namespace CustomIntStackUnitTests
{
    [TestClass]
    public class CustomStackTests
    {
        [TestMethod]
        public void Count_StackOfFiveElements_Five()
        {
            var actual = new CustomStack(1,2,3,4,5);
            int expected = 5;

            int result = actual.Count;

            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void Count_StackOfZeroElements_Zero()
        {
            var actual = new CustomStack();
            int expected = 0;

            int result = actual.Count;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Push_StackOfThreeElements_StackOfFourElements()
        {
            var actual = new CustomStack(1,2,3);
            var expected = new CustomStack(1,2,3,4);

            actual.Push(4);

            Assert.AreEqual(actual.Count, expected.Count);
            Assert.AreEqual(actual.Equals(expected), true);
        }

        [TestMethod]
        public void Pop_ThreeTwoOneStack_Three()
        {
            var actual = new CustomStack(1,2,3);

            var result = actual.Pop();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Peek_ThreeTwoOneStack_Three()
        {
            var actual = new CustomStack(1, 2, 3);

            var result = actual.Peek();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Contains_OneTwoThreeFourFiveContainThree_True()
        {
            var actual = new CustomStack(1,2,3,4,5);

            var result = actual.Contains(3);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Contains_OneTwoThreeFourFiveContainTen_False()
        {
            var actual = new CustomStack(1, 2, 3, 4, 5);

            var result = actual.Contains(10);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetEnumerator_OneTwoMinusThreeFourFive_FiveFour()
        {
            var actual = new CustomStack(1,2,-3,4,5);
            var expected = new []{5,4,0,0,0};

            var result = new int[5];
            int i = 0;
            foreach (var element in actual)
            {
                result[i++] = element;
            }

            CollectionAssert.AreEqual(expected, result);
        }

    }
}
