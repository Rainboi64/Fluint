// 
// ModuleManifest.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Layer.DependencyInjection;

public class ModuleManifest
{
    public ModuleManifest(IEnumerable<ModulePair> modules)
    {
        Modules = modules;
    }

    public IEnumerable<ModulePair> Modules
    {
        get;
        init;
    }

    public override string ToString()
    {
        var table = new ConsoleTable();
        table.AddColumn(new[] { "Module Interface", "Module Implementation", "Assembly", "Initialization Mode" });

        var sortedAlphabetically = Modules.OrderBy(module => module.Abstraction);

        foreach (var pair in sortedAlphabetically)
        {
            table.AddRow(pair.Abstraction, pair.Implementation, Path.GetFileName(pair.AssemblyPath),
                pair.InitializationMethod);
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Loaded Modules: ");
        stringBuilder.AppendLine(Modules.Count().ToString());
        stringBuilder.AppendLine(table.ToMarkDownString());

        return stringBuilder.ToString();
    }
}