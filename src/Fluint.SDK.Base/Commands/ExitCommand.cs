//
// ExitCommand.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer;
using Fluint.Layer.SDK;

namespace Fluint.Implementation.SDK.Commands
{
    [Module("Exit Command", "Exits the application with code 0", "enter this command to exit the application")]
    public class ExitCommand : ICommand
    {
        public string Command => "exit";

        public void Do(string[] args)
        {
            Environment.Exit(0);
        }
    }
}