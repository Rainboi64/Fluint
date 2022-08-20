// 
// IFile.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.VirtualFileSystem;

public interface IDirectory
{
    void AddDirectory(string virtualPath, IDirectory directory);
    IDirectory AddDirectory(string virtualPath, string physicalPath);
    
    void AddFile(string virtualPath, IFile file);
    IFile AddFile(string virtualPath, string physicalPath);
}