//
// ITexture.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITexture : IModule, IDisposable
    {
        int Handle { get; }
        void Bind();
        void Unbind();
        Vector2i Size { get; }
        Color[] Pixels { get; }
        int ConvertIndex(int x, int y);
        void Upload();
    }
}
