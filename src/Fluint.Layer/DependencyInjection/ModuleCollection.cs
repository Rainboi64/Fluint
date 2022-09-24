//
// ModuleCollection.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;

namespace Fluint.Layer.DependencyInjection
{
    public class ModuleCollection
    {
        private readonly List<Type> _instances = new();
        private readonly Dictionary<Type, Type> _mappings = new();
        private readonly Dictionary<Type, Type> _singletonMappings = new();

        public ModulePacket GenerateModulePacket(IRuntime runtime)
        {
            return new ModulePacket(runtime, _mappings, _singletonMappings, _instances);
        }

        public void CreateModuleManifest()
        {
        }

        public void MapScoped(Type abstraction, Type implementation)
        {
            _mappings[abstraction] = implementation;
        }

        public void MapSingleton(Type abstraction, Type implementation)
        {
            _singletonMappings[abstraction] = implementation;
        }

        public void AddInstanced(Type instanced)
        {
            _instances.Add(instanced);
        }

        public void MapScoped<TAbstraction, TImplementation>()
            where TAbstraction : IModule
            where TImplementation : IModule
        {
            var abstractionType = typeof(TAbstraction);
            var implementationType = typeof(TImplementation);
            MapScoped(abstractionType, implementationType);
        }

        public void MapSingleton<TAbstraction, TImplementation>()
            where TAbstraction : IModule
            where TImplementation : IModule
        {
            var abstractionType = typeof(TAbstraction);
            var implementationType = typeof(TImplementation);
            MapSingleton(abstractionType, implementationType);
        }

        public void AddInstanced<TInstancedType>()
        {
            var instancedType = typeof(TInstancedType);
            AddInstanced(instancedType);
        }
    }
}