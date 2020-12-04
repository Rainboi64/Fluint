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

        public ModulePacket(Dictionary<Type, Type> mappings, Dictionary<Type, Type> singletonMappings)
        {
            _mappings = mappings;
            _singletonMappings = new Dictionary<Type, IModule>();
            foreach (var pair in singletonMappings)
            {
                var key = pair.Key;

                var target = ResolveType(key);
                var constructor = target.GetConstructors()[0];
                var parameters = constructor.GetParameters();

                List<object> resolvedParameters = new List<object>();

                foreach (var item in parameters)
                {
                    resolvedParameters.Add(GetScoped(item.ParameterType));
                }

                var value = (IModule)constructor.Invoke(resolvedParameters.ToArray());
                 
                _singletonMappings.Add(key, value);
            }
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
            var target = ResolveType(type);
            var constructor = target.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            List<object> resolvedParameters = new List<object>();

            foreach (var item in parameters)
            {
                resolvedParameters.Add(GetScoped(item.ParameterType));
            }

            return constructor.Invoke(resolvedParameters.ToArray());
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
    }
}
