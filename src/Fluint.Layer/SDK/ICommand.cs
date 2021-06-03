//
// ICommand.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Instanced)]
    public interface ICommand : IModule
    {
        string Command { get; }
        void Do(string[] args);
    }
}
