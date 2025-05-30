// 
// GL46CanvasRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL33;

public class GL33CanvasRenderer : ICanvasRenderer
{
    private ICanvas _canvas;
    private GL33Texture _texture;

    public ITexture Texture => _texture;

    public ICanvas Canvas
    {
        get => _canvas;
        set
        {
            _canvas = value;
            _texture = new GL33Texture(_canvas.Width, Canvas.Height, Filter.Linear);
            _texture.SetData(_canvas.Pixels);
        }
    }

    public void Update()
    {
        GL.BindTexture(TextureTarget.Texture2D, _texture.Handle);
        _texture.SetData(_canvas.Pixels);
    }
}