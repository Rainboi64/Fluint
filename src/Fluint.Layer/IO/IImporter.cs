//
// IImporter.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.IO
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IImporter : IModule
    {
        string[] FileExtenstions { get; }
        IMesh[] Import(string fileName);
    }
}
