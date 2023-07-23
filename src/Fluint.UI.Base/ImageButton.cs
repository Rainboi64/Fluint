// 
// ImageButton.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace Fluint.UI.Base;

public class ImageButton : IImageButton
{
    private readonly ILogger _logger;
    private readonly ITextureFactory _textureFactory;

    private string _path;
    private ITexture _texture;

    public ImageButton(ILogger logger, IGraphicsFactory graphicsFactory)
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
            if (ImGui.ImageButton(Name, new IntPtr(_texture.Handle), new Vector2(Size.X, Size.Y), Vector2.Zero,
                    Vector2.One,
                    new Vector4(BackgroundColor.Red, BackgroundColor.Green, BackgroundColor.Blue,
                        BackgroundColor.Alpha)))
            {
                OnClick();
            }

            return;
        }

        _logger.Error("[{0}] Texture is null", $"Image:{Name}");
    }

    public Vector2i Size
    {
        get;
        set;
    }

    public string Text
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

    public Color4 BackgroundColor
    {
        get;
        set;
    }

    public float Padding
    {
        get;
        set;
    }

    public Action OnClick
    {
        get;
        set;
    }
}