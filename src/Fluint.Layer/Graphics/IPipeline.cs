//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    public interface IPipeline
    {
        PrimitiveTopology PrimitiveTopology { get; }

        IShader VertexShader { get; }

        IShader PixelShader { get; }

        IInputLayout InputLayout { get; }

        IRasterizerState RasterizerState { get; }

        IDepthStencilState DepthStencilState { get; }

        IBlendState BlendState { get; }

        Viewport Viewport { get; }
    }
}