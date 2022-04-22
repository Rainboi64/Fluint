//
// IIOManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Engine;

namespace Fluint.Layer.IO
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IIoManager : IModule
    {
        IMesh[] Import(string fileName);
        void Export(string fileName, IMesh[] meshes, string format = "");
        string[] QueryImportableFormats();
        string[] QueryExportableFormats();
    }
}