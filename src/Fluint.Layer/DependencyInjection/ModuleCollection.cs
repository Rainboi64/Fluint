using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.DependencyInjection
{
    public class ModuleCollection
    {
        private ModulePacket _modulePacket;
        public ModulePacket ModulePacket
        {
            get
            {
                if (_modulePacket is null) _modulePacket = GenerateModulePacket();
                return _modulePacket;
            }
        }

        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _singletonMappings = new Dictionary<Type, Type>();
        private readonly List<Type> _instances = new List<Type>();
        private ModulePacket GenerateModulePacket()
        {
            return new ModulePacket(_mappings, _singletonMappings, _instances);
        }

        public void MapScoped(Type abstraction, Type implementation)
        {
            _mappings.Add(abstraction, implementation);
        }

        public void MapSingleton(Type abstraction, Type implementation)
        {
            _singletonMappings.Add(abstraction, implementation);
        }

        public void AddInstanced(Type instanced)
        {
            _instances.Add(instanced);
        }

        public void MapScoped<Abstraction, Implementation>()
            where Abstraction : IModule
            where Implementation : IModule
        {
            var abstractionType = typeof(Abstraction);
            var implementationType = typeof(Implementation);
            MapScoped(abstractionType, implementationType);
        }

        public void MapSingleton<Abstraction, Implementation>()
            where Abstraction : IModule
            where Implementation : IModule
        {
            var abstractionType = typeof(Abstraction);
            var implementationType = typeof(Implementation);
            MapSingleton(abstractionType, implementationType);
        }
        public void AddInstanced<InstancedType>() 
        {
            var instancedType = typeof(InstancedType);
            AddInstanced(instancedType);
        }

    }
}
