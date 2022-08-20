//
// IImporter.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics;

namespace Fluint.Layer.IO
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IImporter : IModule
    {
        string[] FileExtenstions
        {
            get;
        }

        IMesh[] Import(string fileName);
    }
}