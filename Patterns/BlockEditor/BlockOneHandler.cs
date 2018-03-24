namespace BlockEditor
{
    using System.Diagnostics;

    public class BlockOneHandler : IHandler
    {
        public IHandler Successor { get; set; }

        public void HandleRequestAction()
        {
            Debug.WriteLine("Do some block1 action(e.g begin speach with hero)");
            if (true)
            {
                Debug.WriteLine("Transfer responsibility to next block");
                this.Successor.HandleRequestAction();
            }
        }
    }
}
