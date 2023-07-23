//
// ModulesManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Layer.DependencyInjection
{
    public static class ModulesManager
    {
        public static ModuleManifest GenerateManifest(string modulesFolder)
        {
            var assemblies = new List<Assembly>();

            // Load the DLLs from the modules directory
            if (Directory.Exists(modulesFolder))
            {
                var files = Directory.GetFiles(modulesFolder).Where(x => x.EndsWith(".dll"));
                foreach (var file in files)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(file);
                        assemblies.Add(assembly);
                    }
                    catch (Exception e)
                    {
                        ConsoleHelper.WriteWarning(
                            $"[MODULE MANAGER WARNING]: Failed to load specific assembly, where an exception was thrown. {e}");
                    }
                }
            }

            var types = assemblies
                .Select(GetModulesFromAssembly)
                .SelectMany(types => types);

            var modules = new List<ModulePair>();

            foreach (var type in types)
            {
                if (!TryGetParentModule(type, out var parent))
                {
                    continue;
                }

                var initializationMethod =
                    parent.GetCustomAttribute<InitializationAttribute>()?.InitializationMethod;

                if (initializationMethod is null)
                {
                    ConsoleHelper.WriteWarning(
                        $"[MODULE MANAGER WARNING]: Initialization method not set for {type.Name}, defaulted to {InitializationMethod.Scoped}");
                    initializationMethod = InitializationMethod.Scoped;
                }

                modules.Add(new ModulePair(
                    parent.FullName, type.FullName,
                    type.Assembly.FullName, type.Assembly.Location,
                    initializationMethod.GetValueOrDefault()));
            }

            modules.Sort((x, y) => (int)x.InitializationMethod - (int)y.InitializationMethod);

            return new ModuleManifest(modules);
        }

        public static ModuleCollection LoadManifest(ModuleManifest manifest)
        {
            var moduleCollection = new ModuleCollection();

            var assemblies = new Dictionary<string, Assembly>();
            var pairs = manifest.Modules.ToList();

            foreach (var module in pairs)
            {
                if (assemblies.ContainsKey(module.AssemblyPath))
                {
                    continue;
                }

                assemblies[module.AssemblyPath] = Assembly.LoadFrom(module.AssemblyPath);
            }

            var types = assemblies.Values
                .Select(GetModulesFromAssembly)
                .SelectMany(types => types);

            var modules = manifest.Modules.ToLookup(x => x.Implementation);

            foreach (var type in types)
            {
                var module = modules[type.FullName].FirstOrDefault();

                if (module is null)
                {
                    continue;
                }

                if (!module.Active)
                {
                    continue;
                }

                if (!TryGetParentModule(type, out var parent))
                {
                    continue;
                }

                // There are a couple of cases you may would want to catch
                // for example you should check if the module has the same parent
                // with the manifest, but I choose not to do so for it's obscurity 
                // and minor performance hit. at the time of writing, 2ms to load.

                switch (module.InitializationMethod)
                {
                    case InitializationMethod.Scoped:
                        moduleCollection.MapScoped(parent, type);
                        break;
                    case InitializationMethod.Singleton:
                        moduleCollection.MapSingleton(parent, type);
                        break;
                    case InitializationMethod.Instanced:
                        moduleCollection.AddInstanced(type);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return moduleCollection;
        }

        private static IEnumerable<Type> GetModulesFromAssembly(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(type => typeof(IModule).IsAssignableFrom(type) && type.IsClass);
        }

        private static bool TryGetParentModule(Type module, out Type parent)
        {
            var interfaces = module.GetInterfaces();

            foreach (var parentInterface in interfaces)
            {
                if (parentInterface == typeof(IModule) || !parentInterface.IsAssignableTo(typeof(IModule)))
                {
                    continue;
                }

                parent = parentInterface;
                return true;
            }

            parent = null;
            return false;
        }


        public static ModuleCollection LoadFolder(string modulesFolder)
        {
            var assemblies = new List<Assembly>();

            // Load the DLLs from the modules directory
            if (Directory.Exists(modulesFolder))
            {
                var files = Directory.GetFiles(modulesFolder).Where(x => x.EndsWith(".dll"));
                assemblies.AddRange(files.Select(Assembly.LoadFrom));
            }
            else
            {
                throw new ArgumentException($"module folder \"{modulesFolder}\" not found");
            }

            var types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!typeof(IModule).IsAssignableFrom(type) || !type.IsClass)
                    {
                        continue;
                    }

                    types.Add(type);
                }
            }

            var moduleCollection = new ModuleCollection();

            foreach (var type in types)
            {
                Type parent = null;

                for (var index = 0; index < type.GetInterfaces().Length; index++)
                {
                    var parentInterface = type.GetInterfaces()[index];
                    if (parentInterface == typeof(IModule) || !parentInterface.IsAssignableTo(typeof(IModule)))
                    {
                        continue;
                    }

                    parent = parentInterface;
                    break;
                }

                if (parent != null)
                {
                    var initializationMethod =
                        parent.GetCustomAttribute<InitializationAttribute>()?.InitializationMethod;

                    if (initializationMethod is null)
                    {
                        ConsoleHelper.WriteWarning(
                            $"[MODULE MANAGER WARNING]: Initialization method not set for {type.Name}, defaulted to {InitializationMethod.Scoped}");
                        initializationMethod = InitializationMethod.Scoped;
                    }

                    switch (initializationMethod)
                    {
                        case InitializationMethod.Scoped:
                            moduleCollection.MapScoped(parent, type);
                            break;
                        case InitializationMethod.Singleton:
                            moduleCollection.MapSingleton(parent, type);
                            break;
                        case InitializationMethod.Instanced:
                            moduleCollection.AddInstanced(type);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return moduleCollection;
        }
    }
}