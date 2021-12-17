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
    [Module("Reload Command", "Restarts Fluint instance", "enter this command to restart the application")]
    public class ReloadCommand : ICommand
    {
        public string Command => "reload";

        public void Do(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
