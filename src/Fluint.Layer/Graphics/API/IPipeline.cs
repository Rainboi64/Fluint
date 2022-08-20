//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IPipeline : IModule, IDisposable
    {
        PrimitiveTopology PrimitiveTopology
        {
            get;
        }

        IShader VertexShader
        {
            get;
        }

        IShader PixelShader
        {
            get;
        }

        IInputLayout InputLayout
        {
            get;
        }

        IRasterizerState RasterizerState
        {
            get;
        }

        IDepthStencilState DepthStencilState
        {
            get;
        }

        IBlendState BlendState
        {
            get;
        }

        Viewport Viewport
        {
            get;
            set;
        }
    }
}