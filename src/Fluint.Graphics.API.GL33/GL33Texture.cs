//
// GL33Texture.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL33;

internal class GL33Texture : ITexture, IDisposable
{
    private readonly int _width;
    private readonly int _height;
    private readonly int _nativeTexture;

    public int Handle => _nativeTexture;

    public TextureView View { get; }


    public void Dispose()
    {
        GL.DeleteTexture(_nativeTexture);
    }

    public GL33Texture(int width, int height, Filter filter, TextureAddressMode textureAddressMode, ref byte[] pixelData)
    {
        _width = width;
        _height = height;
        _nativeTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _nativeTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _width, _height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixelData);

        View = new GL33TextureView(this);
    }

    public GL33Texture(int width, int height, Filter filter)
    {
        
        _width = width;
        _height = height;
        _nativeTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _nativeTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _width, _height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

        View = new GL33TextureView(this);
    }

    public GL33Texture(int width, int height, Filter nearest, TextureAddressMode textureAddressMode)
    {
        _width = width;
        _height = height;
        _nativeTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _nativeTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _width, _height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

        View = new GL33TextureView(this);    }

    public void SetData<T>(T[] data) where T : struct
    {
        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba,
            _width,
            _height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            data);
    }
        
    public static implicit operator int(GL33Texture texture)
    {
        return texture._nativeTexture;
    }
}
