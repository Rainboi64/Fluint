using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluint.Layer.DependencyInjection
{
    public class ModulePacket
    {
        private readonly Dictionary<Type, Type> _mappings;
        private readonly Dictionary<Type, IModule> _singletonMappings;
        private readonly List<IModule> _instances;
        public ModulePacket(Dictionary<Type, Type> mappings, Dictionary<Type, Type> singletonMappings, List<Type> instances)
        {
            _mappings = mappings;
            _singletonMappings = new Dictionary<Type, IModule>();
            foreach (var pair in singletonMappings)
            {
                var key = pair.Key;
                var value = (IModule)CreateInstance(pair.Value);
                _singletonMappings.Add(key, value);
            }
            _instances = new List<IModule>();
            foreach (var type in instances)
            {
                _instances.Add((IModule)CreateInstance(type));
            }
        }

        private object Get(Type type)
        {
            if (_mappings.ContainsKey(type)) return GetScoped(type);
            if (_singletonMappings.ContainsKey(type)) return GetSingleton(type);
            if (type == typeof(IEnumerable<IModule>)) return GetInstances();
            return Activator.CreateInstance(type);
        }

        private object CreateInstance(Type target)
        {
            var constructor = target.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            List<object> resolvedParameters = new List<object>();

            foreach (var item in parameters)
            {
                resolvedParameters.Add(Get(item.ParameterType));
            }

            return constructor.Invoke(resolvedParameters.ToArray());
        }

        private Type ResolveType(Type type) 
        {
            if (_mappings.Keys.Contains(type))
            {
                return _mappings[type];
            }

            return type;
        }

        private object GetScoped(Type type)
        {
            return CreateInstance(ResolveType(type));
        }

        private object GetSingleton(Type type)
        {
            return _singletonMappings[type];
        }

        public T GetScoped<T>() where T : IModule
        {
            var type = typeof(T);
            return (T)GetScoped(type);
        }

        public T GetSingleton<T>() where T : IModule 
        {
            var type = typeof(T);
            return (T)GetSingleton(type);
        }

        public IEnumerable<IModule> GetInstances() 
        {
            return _instances;
        }
    }
}
