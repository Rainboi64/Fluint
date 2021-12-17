//
// IExporter.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Engine;

namespace Fluint.Layer.IO
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IExporter : IModule
    {
        string[] FileExtenstions { get; }
        void Export(IMesh[] meshes, string fileName);
    }
}
