//
// ITexture.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API;

[Initialization(InitializationMethod.Scoped)]
public interface ITexture : IModule, IDisposable
{
    int Handle
    {
        get;
    }

    TextureView View
    {
        get;
    }

    void SetData<T>(T[] data) where T : struct;
}