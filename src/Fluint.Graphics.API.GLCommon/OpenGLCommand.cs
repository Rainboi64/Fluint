using System.Drawing;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

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

    public int InputLayout;

    public string Name;

    public int Pipeline;

    public PrimitiveType PrimitiveType;

    public Rectangle ScissorRectangle;

    public int TextureView;

    public CommandType Type;

    public int VertexBuffer;

    public int VertexStride;

    public Rectangle Viewport;
}