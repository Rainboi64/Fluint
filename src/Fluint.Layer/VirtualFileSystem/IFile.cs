// 
// IFile.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.VirtualFileSystem;

public interface IFile
{
    object Data
    {
        get;
    }
}