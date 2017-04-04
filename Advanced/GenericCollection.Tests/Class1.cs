//using System;
//using System.Collections.Generic;
//using NUnit.Framework;
//
//namespace GenericCollection.Tests
//{
//    public class Class1
//    {
//        public IEnumerable<TestCaseData> StructEnqueuElemData
//        {
//            get
//            {
//                yield return new TestCaseData
//                    (new[] { 1, 2, 3 }, 13,
//                        new CustomQueue<int>(1, 2, 3, 13)).Returns(true);
//            }
//        }
//
//        [Test, TestCaseSource("StructEnqueuElemData")]
//        public bool EnqueueElemTest(int[] arr, int elem, CustomQueue<int> expectedQueue)
//        {
//            CustomQueue<int> queue = new CustomQueue<int>(arr);
//            queue.Enqueue(elem);
//            return IsTheSame(queue, expectedQueue);
//        }
//
//        private static bool IsTheSame<T>(CustomQueue<T> queue1, CustomQueue<T> queue2) where T : IComparable<T>
//        {
//            foreach (var variable in queue1)
//            {
//                if (queue1.Peek().GetType() != queue2.Peek().GetType() && !queue1.Dequeue().Equals(queue2.Dequeue()))
//                {
//                    return false;
//                }
//            }
//            return true;
//        }
//    }
//}
