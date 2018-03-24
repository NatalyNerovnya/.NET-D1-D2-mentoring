namespace BlockEditor
{
    using System;

    public interface IHandler
    {
        IHandler Successor { get; set; }

        void HandleRequestAction();
    }
}
