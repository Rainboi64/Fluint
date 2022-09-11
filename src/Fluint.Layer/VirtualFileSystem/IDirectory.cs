// 
// IFile.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.VirtualFileSystem;

[Initialization(InitializationMethod.Scoped)]
public interface IDirectory : IModule
{
    public event EventHandler<DirectoryChangedEvent> OnChange;

    bool TryGetFile(string name, out IFile file);
    bool TryGetDirectory(string name, out IDirectory directory);

    IFile GetFile(string name);
    IDirectory GetDirectory(string name);

    bool HasFile(string name);
    bool HasDirectory(string name);

    void AddDirectory(string name, IDirectory directory);
    IDirectory AddDirectory(string name, string physicalPath);

    void AddFile(string name, IFile file);
    IFile AddFile(string name, string physicalPath);

    void UpdateDirectory(string physicalPath);
}