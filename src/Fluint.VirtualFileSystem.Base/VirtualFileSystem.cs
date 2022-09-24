// 
// VirtualFileSystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.VirtualFileSystem;

namespace Fluint.VirtualFileSystem.Base;

public class VirtualFileSystem : IVirtualFileSystem
{
    public VirtualFileSystem(ModulePacket packet)
    {
        Root = packet.CreateScoped<IDirectory>();
    }

    public IDirectory Root
    {
        get;
    }

    public void LoadFromFile(string path)
    {
        Root.UpdateDirectory(path);
    }

    public void SaveToFile(string path)
    {
        throw new NotImplementedException();
    }

    public IFile QueryFile(string path)
    {
        throw new NotImplementedException();
    }

    public IDirectory QueryDirectory(string path)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return Root.ToString();
    }
}