using System;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;

namespace Fluint.UI.Base;

public class TextureView : ITextureView
{
    private readonly ILogger _logger;

    public TextureView(ILogger logger)
    {
        _logger = logger;
    }

    public string Title
    {
        get;
        set;
    }

    public string Name
    {
        get;
        private set;
    }

    public Vector2i Size
    {
        get;
        set;
    }

    public ITexture Texture
    {
        get;
        set;
    }

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
        if (Texture is not null)
        {
            ImGui.Begin($"{Name}###{Title}");
            ImGui.Image(new IntPtr(Texture.Handle), new Vector2(Size.X, Size.Y));
            ImGui.End();

            return;
        }

        _logger.Warning("[{0}] Texture is null", "TextureView");
    }
}