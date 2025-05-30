using System;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Color4 = OpenTK.Mathematics.Color4;
using Rectangle = System.Drawing.Rectangle;

namespace Fluint.Graphics.API.GLCommon;

public class OpenGlCommand
{
    public Color4 ClearColor;

    public float ClearDepth;

    public byte ClearStencil;

    public int ConstantBuffer;

    public int DrawIndexCount;

    public int DrawIndexOffset;

    public int DrawVertexCount;

    public int DrawVertexOffset;

    public int IndexBuffer;

    public IInputLayout InputLayout;
    public int VertexShader;

    public string Name;

    public int Pipeline;

    public PrimitiveType PrimitiveType;

    public Rectangle ScissorRectangle;

    public int TextureView;

    public CommandType Type;

    public int VertexBuffer;

    public int VertexStride;

    public Viewport Viewport;

    public override string ToString()
    {
        switch (Type)
        {
            case CommandType.Begin:
                return $"{Type}: Pass: {Name}";
            case CommandType.End:
                return $"{Type}";
            case CommandType.Draw:
                return $"{Type}";
            case CommandType.DrawIndexed:
                return $"{Type}: IndexCount: {DrawIndexCount} VertexCount: {DrawVertexCount}";
            case CommandType.SetViewport:
                return $"{Type}: Viewport: {Viewport}";
            case CommandType.SetScissor:
                return $"{Type}: ScissorX: {ScissorRectangle.X} ScissorY: {ScissorRectangle.Y} ScissorWidth: {ScissorRectangle.Width} ScissorHeight: {ScissorRectangle.Height}";
            case CommandType.SetPrimitiveTopology:
                return $"{Type}: PrimitiveType: {PrimitiveType}";
            case CommandType.SetInputLayout:
                return $"{Type}: InputLayout: {InputLayout}";
            case CommandType.SetDepthStencilState:
                break;
            case CommandType.SetRasterizerState:
                break;
            case CommandType.SetBlendState:
                break;
            case CommandType.SetVertexBuffer:
                return $"{Type}: Buffer: {VertexBuffer} Stride: {VertexStride}";
            case CommandType.SetIndexBuffer:
                return $"{Type}: Buffer: {IndexBuffer}";
            case CommandType.SetVertexShader:
                break;
            case CommandType.SetPixelShader:
                break;
            case CommandType.SetComputeShader:
                break;
            case CommandType.SetConstantBuffers:
                return $"{Type}: Buffer: {ConstantBuffer}";
            case CommandType.SetSamplers:
                break;
            case CommandType.SetTextures:
                return $"{Type}: Texture: {TextureView}";
            case CommandType.SetRenderTarget:
                break;
            case CommandType.SetRenderTargets:
                break;
            case CommandType.ClearRenderTarget:
                return $"{Type}: Color: {ClearColor}";
            case CommandType.ClearDepthStencil:
                return $"{Type}: Depth: {ClearDepth} Stencil: {ClearStencil}";
            case CommandType.SetPipeline:
                return $"{Type}: Pipeline: {Pipeline}";
            default:
                return base.ToString();
        }
        return base.ToString();
    }
}