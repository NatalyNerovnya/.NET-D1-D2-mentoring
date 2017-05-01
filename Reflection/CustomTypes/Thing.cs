using MEFContainer;

namespace CustomTypes
{
    public class Thing : IThing
    {
        [Import]
        public SimplyClass Simply { get; set;}

        [Import]
        public ISimplyClassWithInterface Simply2 { get; set; }

        public Thing(IThingParameter thing)
        {
        }
    }
}
