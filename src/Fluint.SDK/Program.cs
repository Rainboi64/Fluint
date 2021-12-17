//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer;
using System;

namespace Fluint.SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Fluint SDK";
            Console.WriteLine("Started Fluint SDK.");
            var packet = ModulesManager.LoadFolder("base").GenerateModulePacket();
            new SDKBase(packet).Listen();
        }
    }
}
