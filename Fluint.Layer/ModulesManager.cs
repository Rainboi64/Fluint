using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using Fluint.Layer.Debugging;
using Fluint.Layer.DependencyInjection;

namespace Fluint.Layer
{
    // only used in implementation
    public class ModulesManager
    {
        public List<IModule> Instances = new List<IModule>();
        public ModuleCollection ModuleCollection { get; private set; }

        public ModulesManager()
        {
            ModuleCollection = new ModuleCollection();
        }

        /// <summary>
        /// Calculates the hashcode representing all of the active plugins
        /// </summary>
        /// <returns>The calculated hashcode</returns>
        public override int GetHashCode()
        {
            return Instances.AsReadOnly().Select(item => item.GetHashCode())
            .Aggregate((total, nextCode) => total ^ nextCode);
        }

        /// <summary>
        /// Loads the modules inside the file using System.Reflection
        /// </summary>
        /// <param name="pluginFolder">The path of the folder to be loaded.</param>
        public void LoadFolder(string pluginFolder)
        {
            //Load the DLLs from the Plugins directory
            if (Directory.Exists(pluginFolder))
            {
                string[] files = Directory.GetFiles(pluginFolder);
                foreach (string file in files)
                {
                    // only loads *.dll
                    if (file.EndsWith(".dll"))
                    {
                        var assembly = Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IModule);
            //Fetch all types that implement the interface IPlugin and are a class
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();

            foreach (var type in types)
            {
                var parent = type.GetInterfaces().Where(x => x.GetInterfaces().FirstOrDefault() == interfaceType).FirstOrDefault();
                var initializationMethod = parent.GetCustomAttribute<InitializationAttribute>().InitializationMethod;
                switch (initializationMethod)
                {
                    case InitializationMethod.Scoped:
                        ModuleCollection.MapScoped(parent, type);
                        Console.WriteLine($"Loaded Scoped in {parent.Name}, {type.Name} from {type.Assembly.FullName}.");
                        break;
                    case InitializationMethod.Singleton:
                        ModuleCollection.MapSingleton(parent, type);
                        Console.WriteLine($"Loaded Singleton in {parent.Name}, {type.Name} from {type.Assembly.FullName}.");
                        break;
                    case InitializationMethod.Instanced:
                        ModuleCollection.AddInstanced(type);
                        Console.WriteLine($"Loaded Instanced in {parent.Name}, {type.Name} from {type.Assembly.FullName}.");
                        break;
                }
            }
        }
    }
}
