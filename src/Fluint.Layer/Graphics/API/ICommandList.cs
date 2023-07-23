//
// ICommandList.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API;

[Initialization(InitializationMethod.Scoped)]
public interface ICommandList
{
    void Clear();

    void Begin(string passName, IPipeline pipeline);

    void ClearRenderTarget(TextureView renderTarget, Color4 clearColor);

    void ClearDepthStencil(TextureView depthStencil, float clearDepth, byte stencilDepth);

    void Draw(int vertexCount);

    void DrawIndexed(int indexCount, int indexOffset, int vertexOffset);

    void End();

    void SetBlendState(IBlendState blendState);

    void SetConstantBuffer(IConstantBuffer buffer, BufferScope bufferScope);

    void SetDepthStencilState(IDepthStencilState depthStencilState);

    void SetIndexBuffer(IIndexBuffer indexBuffer);

    void SetInputLayout(IInputLayout inputLayout);

    void SetPixelShader(IShader pixelShader);

    void SetPrimitiveTopology(PrimitiveTopology primitiveTopology);

    void SetRasterizerState(IRasterizerState rasterizerState);

    void SetRenderTarget(TextureView renderTargets, TextureView depthStencilView);

    void SetRenderTargets(TextureView[] renderTargets, TextureView depthStencilView);

    void SetSampler(ISampler sampler);

    void SetScissorRectangle(Rectangle rectangle);

    void SetTexture(TextureView textureView);

    void SetVertexBuffer(IVertexBuffer vertexBuffer);

    void SetVertexShader(IShader vertexShader);

    void SetViewport(Viewport viewport);

    void Submit();
}