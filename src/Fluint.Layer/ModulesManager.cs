//
// ModulesManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Layer
{
    public static class ModulesManager
    {
        public static ModuleCollection LoadFolder(string modulesfolder)
        {
            var loadingWatch = new Stopwatch();
            loadingWatch.Start();

            var assemblies = new List<Assembly>();

            // Load the DLLs from the modules directory
            if (Directory.Exists(modulesfolder))
            {
                var files = Directory.GetFiles(modulesfolder).Where(x => x.EndsWith(".dll"));
                assemblies.AddRange(files.Select(Assembly.LoadFrom));
            }
            else
            {
                throw new ArgumentException($"module folder \"{modulesfolder}\" not found");
            }

            loadingWatch.Stop();

            var sortingWatch = new Stopwatch();

            sortingWatch.Start();

            var types = new List<Type>();
            var usefulAssemblies = new List<Assembly>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!typeof(IModule).IsAssignableFrom(type) || !type.IsClass)
                    {
                        continue;
                    }

                    types.Add(type);

                    if (!usefulAssemblies.Contains(assembly))
                    {
                        usefulAssemblies.Add(assembly);
                    }
                }
            }

            var table = new ConsoleTable();
            table.AddColumn(new[] { "Stage Name", "Stage Parent", "Assembly", "Initialization Mode" });

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
                            table.AddRow(type.FullName, parent.FullName, Path.GetFileName(type.Assembly.Location),
                                "Scoped");
                            break;
                        case InitializationMethod.Singleton:
                            moduleCollection.MapSingleton(parent, type);
                            table.AddRow(type.FullName, parent.FullName, Path.GetFileName(type.Assembly.Location),
                                "Singleton");
                            break;
                        case InitializationMethod.Instanced:
                            moduleCollection.AddInstanced(type);
                            table.AddRow(type.FullName, parent.FullName, Path.GetFileName(type.Assembly.Location),
                                "Instanced");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            sortingWatch.Stop();

            ConsoleHelper.WriteInfo(table.ToMarkDownString());
            ConsoleHelper.WriteWrappedHeader(
                $"Loaded {types.Count} module from {usefulAssemblies.Count} DLL in \"{modulesfolder}\". Loading:{sortingWatch.ElapsedMilliseconds}ms, Sorting: {loadingWatch.ElapsedMilliseconds}ms. Instance Fingerprint: {moduleCollection.GetHashCode()}");

            return moduleCollection;
        }
    }
}