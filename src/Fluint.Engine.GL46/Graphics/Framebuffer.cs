using System;
using OpenTK.Graphics.OpenGL4;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Diagnostics;

namespace Fluint.Engine.GL46.Graphics
{
    public class Framebuffer : IFramebuffer
    {
        private readonly ILogger _logger;
        public Framebuffer(ILogger logger)
        {
            _logger = logger;
        }

        public int Handle { get; private set; }
        public Vector2i Size { get; private set; }
        private Texture _texture;
        public void Create(Vector2i size)
        {
            Size = size;
            Handle = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
            _texture = new Texture(Size.X, Size.Y);

            if(GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                _logger.Error("[{0}] Error creating framebuffer", "OpenGL46");
                // throw new EngineAPIException("GL46", "Couldn't Create Framebuffer");
            }
        }

        public ITexture GetTexture()
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _texture.Handle, 0);
            return _texture;
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        ~Framebuffer()
        {
            GL.DeleteFramebuffer(Handle);
        }
    }
}