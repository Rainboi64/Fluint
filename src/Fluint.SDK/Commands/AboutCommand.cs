//
// AboutCommand.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.SDK;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Fluint.SDK.Commands
{
    [Layer.Module("About Command", "Returns info about the loaded assembly.", "Returns info about the loaded assembly, doesn't support arguments")]
    public class AboutCommand : ICommand
    {
        public string Command => "about";

        public void Do(string[] args)
        {
            Console.WriteLine($"Working Assembly: {Assembly.GetExecutingAssembly().GetName()}");
        }
    }
}
