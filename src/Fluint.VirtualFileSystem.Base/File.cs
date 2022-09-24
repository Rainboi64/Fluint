// 
// File.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.VirtualFileSystem;

namespace Fluint.VirtualFileSystem.Base;

public class File : IFile
{
    private byte[] _bytes;

    public void Update(string file)
    {
        OnChange?.Invoke(this, new FileChangedEvent());
        Bytes = System.IO.File.ReadAllBytes(file);
    }

    public byte[] Bytes
    {
        get => _bytes;
        private set
        {
            _bytes = value;
            OnChange?.Invoke(this, new FileChangedEvent());
        }
    }

    public event EventHandler<FileChangedEvent> OnChange;
}