using System;
using MEFContainer;
using CustomTypes;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register<IThing, Thing>();
            container.Register<IThingParameter, ThingParameter>();
            var thing = container.Resolve<IThing>();
            Console.ReadKey();
        }
    }
}
