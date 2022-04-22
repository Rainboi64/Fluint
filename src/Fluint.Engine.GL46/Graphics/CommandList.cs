//
// CommandList.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Engine.GL46.Graphics
{
    public class CommandList : ICommandList
    {
        private Stack<OpenGlCommand> _commands = new();

        public void Begin(string passName, IPipeline pipeline)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void ClearDepthStencil(TextureView depthStencil, float clearDepth, byte stencilDepth)
        {
            throw new NotImplementedException();
        }

        public void ClearRenderTarget(TextureView renderTarget, Color4 clearColor)
        {
            throw new NotImplementedException();
        }

        public void Draw(int vertexCount)
        {
            throw new NotImplementedException();
        }

        public void DrawIndexed(int indexCount, int indexOffset, int vertexOffset)
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void SetBlendState(IBlendState blendState)
        {
            throw new NotImplementedException();
        }

        public void SetConstantBuffer(IConstantBuffer buffer, BufferScope bufferScope)
        {
            throw new NotImplementedException();
        }

        public void SetDepthStencilState(IDepthStencilState depthStencilState)
        {
            throw new NotImplementedException();
        }

        public void SetIndexBuffer(IIndexBuffer indexBuffer)
        {
            throw new NotImplementedException();
        }

        public void SetInputLayout(IInputLayout inputLayout)
        {
            throw new NotImplementedException();
        }

        public void SetPixelShader(IShader pixelShader)
        {
            throw new NotImplementedException();
        }

        public void SetPrimitiveTopology(PrimitiveTopology primitiveTopology)
        {
            throw new NotImplementedException();
        }

        public void SetRasterizerState(IRasterizerState rasterizerState)
        {
            throw new NotImplementedException();
        }

        public void SetRenderTarget(TextureView renderTargets, TextureView depthStencilView)
        {
            throw new NotImplementedException();
        }

        public void SetRenderTargets(TextureView[] renderTargets, TextureView depthStencilView)
        {
            throw new NotImplementedException();
        }

        public void SetSampler(ISampler sampler)
        {
            throw new NotImplementedException();
        }

        public void SetScissorRectangle(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void SetTexture(TextureView textureView)
        {
            throw new NotImplementedException();
        }

        public void SetVertexBuffer(IVertexBuffer vertexBuffer)
        {
            throw new NotImplementedException();
        }

        public void SetVertexShader(IShader vertexShader)
        {
            throw new NotImplementedException();
        }

        public void SetViewport(Viewport viewport)
        {
            throw new NotImplementedException();
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }
    }
}