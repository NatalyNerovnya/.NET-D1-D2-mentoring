namespace BlockEditor.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ChainOfResponsibilityTest()
        {
            var block1 = new BlockOneHandler();
            var block2 = new BlockTwoHandler();

            block1.Successor = block2;

            block1.HandleRequestAction();
        }
    }
}
