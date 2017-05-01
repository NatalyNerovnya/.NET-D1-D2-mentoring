using MEFContainer;

namespace CustomTypes
{
    public interface ISimplyClassWithInterface { }
    [Export(typeof(ISimplyClassWithInterface))]
    public class SimplyClassWithInterface : ISimplyClassWithInterface
    {
    }
}
