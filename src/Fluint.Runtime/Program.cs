//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteEmbeddedColorLine($"\n[red]Welcome to Fluint.[/red]\nStart-line called at {DateTime.Now}");
            FluintStarter.Start();
        }
    }
}
