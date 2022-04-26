using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;

namespace Fluint.Engine.GLCommon.UI;

public class Viewport : IViewport
{
    private readonly ModulePacket _packet;
    public readonly IFramebuffer Framebuffer;
    private ITextureView _textureView;

    public Viewport(ModulePacket packet, IFramebuffer framebuffer)
    {
        Framebuffer = framebuffer;
        _packet = packet;
    }

    public string Name
    {
        get;
        set;
    }

    public ICollection<IGuiComponent> Children => throw new NotImplementedException();

    public Vector2i Size
    {
        get;
        set;
    }

    public void Begin(string name)
    {
        Name = name;

        Framebuffer.Create(Size);

        _textureView = _packet.CreateScoped<ITextureView>();
        _textureView.Begin(name);
    }

    public void Tick()
    {
        Framebuffer.Bind();

        _textureView.Texture = Framebuffer.GetTexture();
        _textureView.Texture.Bind();
        _textureView.Tick();
        _textureView.Texture.Unbind();

        Framebuffer.Unbind();
    }
}