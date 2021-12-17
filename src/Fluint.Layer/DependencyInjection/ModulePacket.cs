//
// ModulePacket.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;

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

        public object FetchAndCreateInstance(Type type)
        {
            if (type == typeof(ModulePacket))
            {
                return this;
            }

            if (_mappings.ContainsKey(type))
            {
                return CreateScoped(type);
            }

            if (_singletonMappings.ContainsKey(type))
            {
                return GetSingleton(type);
            }

            if (type == typeof(IEnumerable<IModule>))
            {
                return GetInstances();
            }

            return CreateInstance(type);
        }

        public object CreateInstance(Type target)
        {
            return CreateInstance(target, target.GetGenericArguments());
        }

        public object CreateInstance(Type target, Type[] generics)
        {
            var constructor = target.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            if (target.ContainsGenericParameters)
            { 
                target = target.MakeGenericType(generics);
            }

            var resolvedParameters = new List<object>();

            foreach (var item in parameters)
            {
                resolvedParameters.Add(FetchAndCreateInstance(item.ParameterType));
            }
            return Activator.CreateInstance(target, resolvedParameters.ToArray());
        }

        private Type ResolveType(Type type) 
        {
           // 😂🤣 WHO DID THIS 🤣😂
           foreach (var pair in _mappings)
           {
               if (pair.Key.Name == type.Name)
               {
                   return pair.Value;
               }
           }
           return type;
        }

        private object CreateScoped(Type type)
        {
            return CreateInstance(ResolveType(type), type.GetGenericArguments());
        }

        private object GetSingleton(Type type)
        {
            return _singletonMappings[type];
        }

        public T CreateScoped<T>() where T : IModule
        {
            var type = typeof(T);
            return (T)CreateScoped(type);
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
