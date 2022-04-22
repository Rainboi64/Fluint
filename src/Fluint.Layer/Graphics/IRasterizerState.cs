//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics
{
    public interface IRasterizerState
    {
        CullMode CullMode { get; }

        FillMode FillMode { get; }

        bool IsDepthEnabled { get; }

        bool IsScissorEnabled { get; }

        bool IsMultiSampleEnabled { get; }

        bool IsAntialiasedLineEnabled { get; }
    }
}