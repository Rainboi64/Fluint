//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API
{
    [Flags]
    public enum TextureViewType
    {
        RenderTarget = 1,
        ShaderResource = 2,
        DepthStencil = 4
    }
}