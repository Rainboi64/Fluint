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
        private readonly List<IModule> _staticModules;
        public readonly Dictionary<Type, Type> ScopedMappings;
        public readonly Dictionary<Type, IModule> SingletonMappings;

        public ModulePacket(IRuntime runtime, Dictionary<Type, Type> scopedMappings,
            Dictionary<Type, Type> singletonMappings, List<Type> instances)
        {
            CurrentRuntime = runtime;
            ScopedMappings = scopedMappings;

            SingletonMappings = new Dictionary<Type, IModule>();

            foreach (var (key, type) in singletonMappings)
            {
                var value = (IModule)CreateInstance(type);
                SingletonMappings[key] = value;
            }

            _staticModules = new List<IModule>();
            foreach (var type in instances)
            {
                _staticModules.Add((IModule)CreateInstance(type));
            }
        }

        public IRuntime CurrentRuntime
        {
            get;
        }

        public object FetchAndCreateInstance(Type type)
        {
            if (type == typeof(ModulePacket))
            {
                return this;
            }

            if (ScopedMappings.ContainsKey(type))
            {
                return CreateScoped(type);
            }

            if (SingletonMappings.ContainsKey(type))
            {
                return GetSingleton(type);
            }

            if (type == typeof(IEnumerable<IModule>))
            {
                return GetInstances();
            }

            return CreateInstance(type);
        }


        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
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
            foreach (var pair in ScopedMappings)
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
            return SingletonMappings[type];
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
            return _staticModules;
        }

        public IEnumerable<T> GetInstances<T>()
        {
            return _staticModules.OfType<T>();
        }
    }
}