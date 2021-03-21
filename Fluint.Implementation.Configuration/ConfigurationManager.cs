using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fluint.Layer.Configuration;
using Newtonsoft.Json;

namespace Fluint.Implementation.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly List<IConfiguration> _confings = new();
        public T Get<T>() where T : IConfiguration
        {
            if (_confings.OfType<T>().Any())
            {
                return _confings.OfType<T>().FirstOrDefault();
            }
            else
            {
               var jsonData = File.ReadAllText(@$".\configs\{typeof(T).Name}.json");
               var obj = JsonConvert.DeserializeObject<T>(jsonData, new Newtonsoft.Json.Converters.StringEnumConverter());
               _confings.Add(obj);
               return obj;
            }
        }

        public void Add(IConfiguration configuration)
        {
            _confings.Add(configuration);
            var jsonData = JsonConvert.SerializeObject(configuration, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
            Directory.CreateDirectory(@$".\configs\");
            File.WriteAllText(@$".\configs\{configuration.GetType().Name}.json", jsonData);
        }

        public void Save()
        {
            foreach (var config in _confings)
            {
                var jsonData = JsonConvert.SerializeObject(config, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
                File.WriteAllText(@$".\configs\{config.GetType().Name}.json", jsonData);
            }
        }

        public bool Contains<T>() where T : IConfiguration
        {
            var length = _confings.Count;
            for (var i = 0; i < length; i++)
            {
                var current = _confings[length - 1];
                if (current.GetType() == typeof(T))
                {
                    return true;
                }
            }

            if (File.Exists(@$".\configs\{typeof(T).Name}.json"))
            {
                return true;
            }

            return false;
        }
    }
}
