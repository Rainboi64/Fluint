//
// IParser.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IParser : IModule
    {
        void Add(ICommand command);
        void Parse(string command, string[] args);
    }
}
