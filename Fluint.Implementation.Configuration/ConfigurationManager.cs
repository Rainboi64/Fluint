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
        private readonly List<IConfiguration> _confings = new List<IConfiguration>();
        public T Get<T>() where T : IConfiguration
        {
            if (_confings.OfType<T>().Any())
            {
                return _confings.OfType<T>().FirstOrDefault();
            }
            else
            {
               var jsonData= File.ReadAllText(@$".\configs\{nameof(T)}.json");
               var obj = JsonConvert.DeserializeObject<T>(jsonData);
               _confings.Add(obj);
               return obj;
            }
        }

        public void Add(IConfiguration configuration)
        {
            _confings.Add(configuration);
            var jsonData = JsonConvert.SerializeObject(configuration);
            Directory.CreateDirectory(@$".\configs\");
            File.WriteAllText(@$".\configs\{configuration.GetType().Name}.json", jsonData);
        }

        public void Save()
        {
            foreach (var config in _confings)
            {
                var jsonData = JsonConvert.SerializeObject(config);
                File.WriteAllText(@$".\configs\" + nameof(config), jsonData);
            }
        }
    }
}
