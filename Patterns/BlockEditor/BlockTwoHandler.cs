namespace BlockEditor
{
    using System.Diagnostics;

    public class BlockTwoHandler : IHandler
    {
        public IHandler Successor { get; set; }

        public void HandleRequestAction()
        {
            Debug.WriteLine("Do some block2 action(e.g show text)");
            if (true)
            {
                Debug.WriteLine("Transfer responsibility to next block");
            }
        }
    }
}
