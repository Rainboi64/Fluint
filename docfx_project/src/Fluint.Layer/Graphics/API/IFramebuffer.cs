//
// IFramebuffer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IFramebuffer : IModule, IDisposable
    {
        Vector2i Size
        {
            get;
        }

        int Handle
        {
            get;
        }

        void Create(Vector2i size);
        void Bind();
        void Unbind();
        ITexture GetTexture();
    }
}