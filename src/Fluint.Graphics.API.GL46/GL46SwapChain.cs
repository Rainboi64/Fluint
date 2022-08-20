// 
// GL46SwapChain.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46;

public class GL46SwapChain : ISwapChain
{
    private readonly int  _nativeFramebuffer;
    private readonly int _rbo;

    private readonly int _nativeTexture;
    // Could possibly be optimized to use RenderBuffers instead.
    // https://learnopengl.com/Advanced-OpenGL/Framebuffers
    public GL46SwapChain(SwapChainDescriptor descriptor)
    {
        _nativeFramebuffer = GL.GenFramebuffer();
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, _nativeFramebuffer);

        var texture = new GL46Texture(descriptor.Width, descriptor.Height, Filter.Nearest);
        _nativeTexture = texture;
        
        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba32f,
            descriptor.Width,
            descriptor.Height,
            0,
            PixelFormat.Rgb,
            PixelType.UnsignedByte,
            IntPtr.Zero);
        
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, texture, 0);
        
        _rbo = GL.GenRenderbuffer();
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _rbo);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, descriptor.Width, descriptor.Height);
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, _rbo);
        
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);
        GL.FrontFace(FrontFaceDirection.Ccw);

        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Less);
        
        TextureView = texture.View;
    }
    
    public void Dispose()
    {
        GL.DeleteFramebuffer(_nativeFramebuffer);
    }

    public TextureView TextureView
    {
        get;
    }

    public TextureView DepthStencilView
    {
        get;
    }

    public void Present()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, _nativeFramebuffer);
    }

    public void Disconnect()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    public void Modify(SwapChainDescriptor descriptor)
    {
        GL.BindTexture(TextureTarget.Texture2D, _nativeTexture);
        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba32f,
            descriptor.Width,
            descriptor.Height,
            0,
            PixelFormat.Rgb,
            PixelType.UnsignedByte,
            IntPtr.Zero);
        
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _rbo);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, descriptor.Width, descriptor.Height);
    }
}