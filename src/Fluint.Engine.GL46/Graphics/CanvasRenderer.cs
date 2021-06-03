//
// CanvasRenderer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46.Graphics
{
    public class CanvasRenderer : ICanvasRenderer
    {
        public ICanvas Canvas { get; set; }

        private const string _vertexShader = @"
#version 330 core

layout(location = 0) in vec3 aPosition;

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main(void)
{
    texCoord = aTexCoord;

    gl_Position = vec4(aPosition, 1.0);
}
";
        private const string _fragmentShader = @"
#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture1;

void main()
{
    outputColor = texture(texture0, texCoord);
}
";

        private static readonly float[] _vertices =
        {
            //Position        Texture coordinates
             1f,  1f, 0f,     1f, 1f, // top right
             1f, -1f, 0f,     1f, 0f, // bottom right
            -1f, -1f, 0f,     0f, 0f, // bottom left
            -1f,  1f, 0f,     0f, 1f  // top left
        };

        private static readonly int[] _indices = {
            0, 1, 3, // first triangle
            1, 2, 3  // second triangle
        };

        private int _vbo = 0;
        private int _ebo = 0;
        private int _texture = 0;
        private Shader _shader;
        private VertexArrayObject<float> _vao;

        public void Create()
        {
            _shader = new Shader();
            _shader.LoadSource(_vertexShader, _fragmentShader);

            _vao = new VertexArrayObject<float>();
            _vao.Load();
            _vao.Calculate();

            _ebo = GL.GenBuffer();

            _texture = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, _texture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _vertices.Length, _vertices, BufferUsageHint.DynamicDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * _indices.Length, _indices, BufferUsageHint.DynamicDraw);

            _vao.Enable();
        }

        private void CreateTexture(Color[] pixelBuffer, int width, int height)
        {
            GL.BindTexture(TextureTarget.Texture2D, _texture);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                width,
                height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixelBuffer);

            GL.ActiveTexture(TextureUnit.Texture0);
        }

        public void Destroy()
        {
            GL.DeleteBuffer(_vbo);
            GL.DeleteBuffer(_ebo);
            _vao.Dispose();
            _shader.Dispose();
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            CreateTexture(Canvas.Pixels, Canvas.Width, Canvas.Height);

            _shader.Enable();
            _shader.Uniform1("baseTexture", 0);

            _vao.Enable();

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, _indices);
        }

        public void Dispose()
        {
            Destroy();
            GC.SuppressFinalize(this);
        }
    }
}
