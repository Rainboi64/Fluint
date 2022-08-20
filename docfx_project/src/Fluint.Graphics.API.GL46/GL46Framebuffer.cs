using System;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46
{
    public class GL46Framebuffer : IFramebuffer
    {
        private readonly ILogger _logger;
        private GL46Texture _gl46Texture;

        public GL46Framebuffer(ILogger logger)
        {
            _logger = logger;
        }

        public int Handle
        {
            get;
            private set;
        }

        public Vector2i Size
        {
            get;
            private set;
        }

        public void Create(Vector2i size)
        {
            Size = size;
            Handle = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
            _gl46Texture = new GL46Texture(Size.X, Size.Y);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                _logger.Error("[{0}] Error creating framebuffer", "OpenGL46");
                // throw new EngineAPIException("GL46", "Couldn't Create GL46Framebuffer");
            }
        }

        public ITexture GetTexture()
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D, _gl46Texture.Handle, 0);
            return _gl46Texture;
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GL46Framebuffer()
        {
            Dispose(false);
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _gl46Texture?.Dispose();
            }
        }
    }
}