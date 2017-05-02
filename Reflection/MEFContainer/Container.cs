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
            foreach (var type in exportTypes)
            {
                var export = type.GetCustomAttribute<ExportAttribute>();
                if (export != null)
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

        public void Register<TKey, TValue>()
        {
            mapper.Add(typeof(TKey), typeof(TValue));
        }

        public void Register(Type type, Type baseType)
        {
            mapper.Add(type, baseType);
        }

        public void AddType(Type type)
        {
            AddType(type, type);
        }

        public void AddType(Type targetType, Type sourceType)
        {
            Register(sourceType, targetType);
        }

        public T Resolve<T>()
        {
            var instance = (T)Resolve(typeof(T));
            ResolveProperties(instance);
            return instance;
        }

        private object Resolve(Type type)
        {
            Type resolvedType;
            if (mapper.TryGetValue(type, out resolvedType) == false)
            {
                Register(type, type);
                resolvedType = type;
            }
            var ctorInfo = resolvedType.GetConstructors().FirstOrDefault();
            if (ctorInfo != null)
            {
                var parameters = ctorInfo.GetParameters();

                return parameters.Any() ?  ctorInfo.Invoke(ResolveParameters(parameters).ToArray()) : Activator.CreateInstance(resolvedType);
            }
            return Activator.CreateInstance(resolvedType);
        }

        private IEnumerable<object> ResolveParameters(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(p => Resolve(p.ParameterType)).ToList();
        }

        private void ResolveProperties(object instance)
        {
            foreach (var property in instance.GetType().GetProperties())
            {
                if (property.IsDefined(typeof(ImportAttribute)))
                {
                    property.SetValue(instance, Resolve(property.PropertyType));
                }
            }
        }
    }
}
