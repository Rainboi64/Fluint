//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API;

public interface IRasterizerState : IDisposable
{
    CullMode CullMode
    {
        get;
    }

    FillMode FillMode
    {
        get;
    }

    bool IsDepthEnabled
    {
        get;
    }

    bool IsScissorEnabled
    {
        get;
    }

    bool IsMultiSampleEnabled
    {
        get;
    }

    bool IsAntialiasedLineEnabled
    {
        get;
    }
}