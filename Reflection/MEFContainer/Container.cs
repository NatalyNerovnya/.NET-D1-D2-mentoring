using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MEFContainer
{
    public class Container
    {
        private readonly Dictionary<Type, Type> mapper;

        public Container()
        {
            mapper = new Dictionary<Type, Type>();
        }

        public void Register<TKey,TValue>()
        {
            mapper.Add(typeof(TKey), typeof(TValue));
        }

        public T Resolve<T>()
        {
            Type type = mapper[typeof(T)];
            return (T)Resolve(type);
        }

        private object Resolve(Type type)
        {
            var resolvedType = mapper[type];
            var ctorInfo = resolvedType.GetConstructors().First();
            var parameters = ctorInfo.GetParameters();

            if (!parameters.Any())
            {
                return Activator.CreateInstance(resolvedType);
            }
            else
            {
                return ctorInfo.Invoke(ResolveParameters(parameters).ToArray());
            }
        }

        private IEnumerable<object> ResolveParameters(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(p => Resolve(p.ParameterType)).ToList();
        }
    }
}
