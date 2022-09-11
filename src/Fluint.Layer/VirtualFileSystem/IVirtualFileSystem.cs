// 
// IVirtualFileSystem.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.VirtualFileSystem;

[Initialization(InitializationMethod.Scoped)]
public interface IVirtualFileSystem : IModule
{
    IDirectory Root
    {
        get;
    }

    IFile QueryFile(string path);
    IDirectory QueryDirectory(string path);

    public void LoadFromFile(string path);
    public void SaveToFile(string path);
}