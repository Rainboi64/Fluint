// 
// Image.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;

namespace Fluint.UI.Base;

public class Image : IImage
{
    private readonly ILogger _logger;
    private readonly ITextureFactory _textureFactory;

    private string _path;
    private ITexture _texture;

    public Image(ILogger logger, IGraphicsFactory graphicsFactory)
    {
        _logger = logger;
        _textureFactory = graphicsFactory.CreateTextureFactory();
    }

    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
        if (_texture is not null)
        {
            ImGui.Image(new IntPtr(_texture.Handle), new Vector2(Size.X, Size.Y));
            return;
        }

        _logger.Error("[{0}] Texture is null", $"Image:{Name}");
    }

    public Vector2i Size
    {
        get;
        set;
    }

    public string Path
    {
        get => _path;
        set
        {
            _path = value;
            _texture = _textureFactory.CreateTextureFromFile(_path, true);
        }
    }
}