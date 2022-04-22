using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;

namespace Fluint.Engine.GL46.UI 
{
    public class Viewport : IViewport
    {
        private readonly ModulePacket _packet;
        public readonly IFramebuffer _framebuffer;
        private ITextureView _textureView;

        public Viewport(ModulePacket packet, IFramebuffer framebuffer)
        {
            _framebuffer = framebuffer;
            _packet = packet;
        }
        
        public string Name { get; set; }
        public ICollection<IGuiComponent> Children => throw new System.NotImplementedException();

        public Vector2i Size { get; set; }

        public void Begin(string name)
        {
            Name = name;

            _framebuffer.Create(Size);

            _textureView = _packet.CreateScoped<ITextureView>();
            _textureView.Begin(name);
        }

        public void Tick()
        {
            _framebuffer.Bind();

            _textureView.Texture = _framebuffer.GetTexture();
            _textureView.Texture.Bind();
            _textureView.Tick();
            _textureView.Texture.Unbind();

            _framebuffer.Unbind();
        }
    }
}