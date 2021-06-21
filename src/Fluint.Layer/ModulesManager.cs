//
// ModulesManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;
using System.Diagnostics;

namespace Fluint.Layer
{
    // only used in implementation
    public static class ModulesManager
    {
        /// <summary>
        /// Loads the modules inside the file using System.Reflection
        /// </summary>
        /// <param name="modulesfolder">The path of the folder to be loaded.</param>
        public static ModuleCollection LoadFolder(string modulesfolder)
        {
            var loadingWatch = new Stopwatch();
            loadingWatch.Start();

            var assemblies = new List<Assembly>();

            // Load the DLLs from the modules directory
            if (Directory.Exists(modulesfolder))
            {
                var files = Directory.GetFiles(modulesfolder).Where(x => x.EndsWith(".dll"));
                foreach (var file in files)
                {
                    try
                    {
                        assemblies.Add(Assembly.LoadFrom(file));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
            {
                throw new ArgumentException($"module folder \"{modulesfolder}\" not found");
            }

            loadingWatch.Stop();

            var sortingWatch = new Stopwatch();

            sortingWatch.Start();

            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IModule).IsAssignableFrom(type) && type.IsClass)
                    {
                        types.Add(type);
                    }
                }
            }

            var table = new ConsoleTable();
            table.AddColumn(new[] { "Type Name", "Type Parent", "Assembly", "Initialization Mode"});

            var moduleCollection = new ModuleCollection();

            foreach (var type in types)
            {
                Type parent = null;

                foreach (var ParentInterface in type.GetInterfaces())
                {
                    if (ParentInterface != typeof(IModule) && ParentInterface.IsAssignableTo(typeof(IModule)))
                    {
                        parent = ParentInterface;
                        break;
                    }
                }

                var initializationMethod = parent.GetCustomAttribute<InitializationAttribute>().InitializationMethod;
                switch (initializationMethod)
                {
                    case InitializationMethod.Scoped:
                        moduleCollection.MapScoped(parent, type);
                        table.AddRow(type.FullName, parent.FullName, Path.GetFileName(type.Assembly.Location), "Scoped");
                        break;
                    case InitializationMethod.Singleton:
                        moduleCollection.MapSingleton(parent, type);
                        table.AddRow(type.FullName, parent.FullName, Path.GetFileName(type.Assembly.Location), "Singleton");
                        break;
                    case InitializationMethod.Instanced:
                        moduleCollection.AddInstanced(type);
                        table.AddRow(type.FullName, parent.FullName, Path.GetFileName(type.Assembly.Location), "Instanced");
                        break;
                }
            }
            sortingWatch.Stop();

            ConsoleHelper.WriteInfo(table.ToMarkDownString());
            ConsoleHelper.WriteWrappedHeader($"Loaded {types.Count} module from {assemblies.Count} DLL in \"{modulesfolder}\". Loading:{sortingWatch.ElapsedMilliseconds}ms, Sorting: {loadingWatch.ElapsedMilliseconds}ms. Instance Fingerprint: {moduleCollection.GetHashCode()}");
           
            return moduleCollection;
        }
    }
}
