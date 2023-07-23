//
// VSync.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Linq;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;
using Fluint.Layer.UI;

namespace Fluint.Graphics.API.GLCommon.Lambdas;

[Module("VSync Toggle Command", "Toggles the virtual sync of the main window", "type the command to toggle vsync")]
public class VSync : ILambda
{
    private readonly ModulePacket _packet;

    public VSync(ModulePacket packet, ILogger logger)
    {
        _packet = packet;
    }

    public string Command => "vsync";

    public LambdaObject Run(string[] args)
    {
        var window = _packet.GetSingleton<IGuiInstanceManager>().Windows.FirstOrDefault();
        if (window is null)
        {
            return LambdaObject.Error("Couldn't find window object");
        }

        window.VSync = !window.VSync;

        return new LambdaObject(!window.VSync
            ? "VSync is ON"
            : "VSync is OFF");
    }
}