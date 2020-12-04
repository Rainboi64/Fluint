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
        private List<IConfiguration> _confings = new List<IConfiguration>();
        public T Get<T>() where T : IConfiguration
        {
            if (_confings.OfType<T>().Any())
            {
                return _confings.OfType<T>().FirstOrDefault();
            }
            else
            {
               var jsonData= File.ReadAllText(@".\config\" + nameof(T));
               var obj = JsonConvert.DeserializeObject<T>(jsonData);
               _confings.Add(obj);
               return obj;
            }
        }

        public void Add(IConfiguration configuration)
        {
            _confings.Add(configuration);
            var jsonData = JsonConvert.SerializeObject(configuration);
            File.WriteAllText(@".\config\" + nameof(configuration), jsonData);
        }

        public void Save()
        {
            foreach (var config in _confings)
            {
                var jsonData = JsonConvert.SerializeObject(config);
                File.WriteAllText(@".\config\" + nameof(config), jsonData);
            }
        }
    }
}
