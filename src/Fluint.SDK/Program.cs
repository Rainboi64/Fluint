//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.IO;
using Fluint.Layer;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;

namespace Fluint.SDK;

internal static class FluintSdkRuntime
{
    private static void Main(string[] args)
    {
        var moduleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "base");
        var manifest = new StartupManifest(args, moduleDirectory);
        var manager = new InstanceManager(manifest);

        manager.CreateInstance<SdkInstance>();
        manager.StartAll();
    }
}