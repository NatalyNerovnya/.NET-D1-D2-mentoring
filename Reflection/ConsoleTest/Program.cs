using System;
using System.Reflection;
using MEFContainer;
using CustomTypes;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            var targetAssembly = Assembly.LoadFrom("CustomTypes.dll");
            container.AddAssembly(targetAssembly);
            
            container.Register<IThing, Thing>();
            container.Register<IThingParameter, ThingParameter>();
            var simple = container.Resolve<SimplyClass>();
            var simpleWithInterface = container.Resolve<ISimplyClassWithInterface>();
            var thing = container.Resolve<IThing>();
            
            Console.ReadKey();
        }
    }
}
