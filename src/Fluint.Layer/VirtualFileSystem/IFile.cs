// 
// IFile.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Text;

namespace Fluint.Layer.VirtualFileSystem;

[Initialization(InitializationMethod.Scoped)]
public interface IFile : IModule
{
    byte[] Bytes
    {
        get;
    }

    public void Update(string file);

    public string ToString()
    {
        var unicode = Encoding.Default;
        return unicode.GetString(Bytes);
    }
}