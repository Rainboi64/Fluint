//
// IFramebuffer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IFramebuffer : IModule
    {
        Vector2i Size { get; } 
        int Handle { get; }
        void Create(Vector2i size);
        void Bind();
        void Unbind();
        ITexture GetTexture();
    }
}