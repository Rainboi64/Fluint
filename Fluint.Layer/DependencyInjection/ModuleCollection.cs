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

        private void MapScoped(Type abstraction, Type implementation)
        {
            _mappings.Add(abstraction, implementation);
        }
        private void MapSingleton(Type abstraction, Type implementation)
        {
            _singletonMappings.Add(abstraction, implementation);
        }
        private ModulePacket GenerateModulePacket()
        {
            return new ModulePacket(_mappings, _singletonMappings);
        }


        public void MapScoped<Abstraction, Implementation>()
            where Abstraction : IModule
            where Implementation : IModule
        {
            var abstractionType = typeof(Abstraction);
            var ImplementationType = typeof(Implementation);
            MapScoped(abstractionType, ImplementationType);
        }

        public void MapSingleton<Abstraction, Implementation>()
            where Abstraction : IModule
            where Implementation : IModule
        {
            var abstractionType = typeof(Abstraction);
            var ImplementationType = typeof(Implementation);
            MapSingleton(abstractionType, ImplementationType);
        }

    }
}
