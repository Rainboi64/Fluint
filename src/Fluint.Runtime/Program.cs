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

namespace Fluint.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            var moduleDirectiory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "base");
            var manager = new InstanceManager(new StartupManifest(args, moduleDirectiory));

            manager.CreateInstance<FluintInstance>();
            manager.CreateInstance<SDKInstance>();
            manager.StartAll();
        }
    }
}
