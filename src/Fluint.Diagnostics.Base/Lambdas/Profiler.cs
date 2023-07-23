//
// Profiler.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.SDK;
using Fluint.Layer.UI;

namespace Fluint.Diagnostics.Base.Lambdas;

public class Profiler : ILambda
{
    private readonly ModulePacket _packet;

    public Profiler(ModulePacket packet, ILogger logger)
    {
        _packet = packet;
    }

    public string Command => "profiler";

    public LambdaObject Run(string[] args)
    {
        var window = _packet.GetSingleton<IGuiInstanceManager>().Windows.FirstOrDefault();
        if (window is null)
        {
            return LambdaObject.Error("Couldn't find window object");
        }

        var details = window.Profiler.GetDetails();
        var table = new ConsoleTable();
        table.AddColumn(new[] { "Keys", "Values" });
        foreach (var pair in details)
        {
            table.AddRow(pair.Key, pair.Value);
        }

        return new LambdaObject(table.ToMarkDownString(), LambdaStatus.Success);
    }
}