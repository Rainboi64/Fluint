// 
// SDK.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using Fluint.Layer;
using Fluint.Layer.Runtime;
using Fluint.Layer.SDK;

namespace Fluint.SDK;

public class Sdk
{
    private const string BaseFileLocation = "base";
    private const string VersionDetails = "pre-α 2022.5.1.00";

    public void Start()
    {
        var moduleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseFileLocation);
        var manifest = new StartupManifest(Environment.GetCommandLineArgs(), moduleDirectory, VersionDetails);
        var manager = new InstanceManager(manifest);

        manager.CreateInstance<SdkInstance>();
        manager.StartAll();
    }
}