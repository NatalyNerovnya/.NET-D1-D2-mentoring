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

        public void AddAssembly(Assembly assembly)
        {
            var exportTypes = assembly.ExportedTypes.Where(t => t.IsClass || t.IsInterface);
            foreach(var type in exportTypes)
            {
                var export = type.GetCustomAttribute<ExportAttribute>();
                if(export != null)
                {
                   if (export.ExportType != null)
                    {
                        Register(export.ExportType, type);
                    }
                    else
                    {
                        Register(type, type);
                    }
                }
            }
        }

        public void Register<TKey,TValue>()
        {
            mapper.Add(typeof(TKey), typeof(TValue));
        }

        public void Register(Type type, Type baseType)
        {
            mapper.Add(type, baseType);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
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
