//
// ImGuiTexture.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Fluint.Graphics.API.GL33.ImGuiImpl;

public enum TextureCoordinate
{
    S = TextureParameterName.TextureWrapS,
    T = TextureParameterName.TextureWrapT,
    R = TextureParameterName.TextureWrapR
}

internal class ImGuiTexture : IDisposable
{
    public const SizedInternalFormat Srgb8Alpha8 = (SizedInternalFormat)All.Srgb8Alpha8;
    public const SizedInternalFormat Rgb32F = (SizedInternalFormat)All.Rgb32f;

        public const GetPName MaxTextureMaxAnisotropy = (GetPName)0x84FF;

    
    public static readonly float MaxAniso;
    public readonly int GlTexture;
    public readonly PixelInternalFormat InternalFormat;
    public readonly int MipmapLevels;

    public readonly string Name;
    public readonly int Width, Height;

    static ImGuiTexture()
    {
        MaxAniso = GL.GetFloat((GetPName)0x84FF);
    }

    public ImGuiTexture(string name, Bitmap image, bool generateMipmaps, bool srgb)
    {
        Name = name;
        Width = image.Width;
        Height = image.Height;
        InternalFormat = srgb ? (PixelInternalFormat)Srgb8Alpha8 : PixelInternalFormat.Rgba;

        if (generateMipmaps)
        {
            // Calculate how many levels to generate for this texture
            MipmapLevels = (int)Math.Floor(Math.Log(Math.Max(Width, Height), 2));
        }
        else
        {
            // There is only one level
            MipmapLevels = 1;
        }

        Util.CheckGlError("Clear");

        GL.GenTextures(1, out GlTexture);
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        var data = image.LockBits(new Rectangle(0, 0, Width, Height),
            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        GL.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat, Width, Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
        Util.CheckGlError("TexImage2D");

        image.UnlockBits(data);

        if (generateMipmaps)
        {
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public ImGuiTexture(string name, int glTex, int width, int height, int mipmaplevels,
        PixelInternalFormat internalFormat)
    {
        Name = name;
        GlTexture = glTex;
        Width = width;
        Height = height;
        MipmapLevels = mipmaplevels;
        InternalFormat = internalFormat;
    }

    public ImGuiTexture(string name, int width, int height, IntPtr data, bool generateMipmaps = false,
        bool srgb = false)
    {
        Name = name;
        Width = width;
        Height = height;
        InternalFormat = srgb ? (PixelInternalFormat)Srgb8Alpha8 : PixelInternalFormat.Rgba8;
        MipmapLevels = generateMipmaps == false ? 1 : (int)Math.Floor(Math.Log(Math.Max(Width, Height), 2));

        GL.GenTextures(1, out GlTexture);
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        GL.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat, Width, Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data);

        if (generateMipmaps)
        {
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        
        
        SetWrap(TextureCoordinate.S, TextureWrapMode.Repeat);
        SetWrap(TextureCoordinate.T, TextureWrapMode.Repeat);

        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void Dispose()
    {
        GL.DeleteTexture(GlTexture);
    }

    public void SetMinFilter(TextureMinFilter filter)
    {
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filter);
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void SetMagFilter(TextureMagFilter filter)
    {
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filter);
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void SetAnisotropy(float level)
    {
        const TextureParameterName textureMaxAnisotropy = (TextureParameterName)0x84FE;
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, textureMaxAnisotropy, Util.Clamp(level, 1, MaxAniso));
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void SetLod(int @base, int min, int max)
    {
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, @base);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinLod, min);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLod, max);
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void SetWrap(TextureCoordinate coord, TextureWrapMode mode)
    {
        GL.BindTexture(TextureTarget.Texture2D, GlTexture);
        GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)coord, (int)mode);
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }
}
