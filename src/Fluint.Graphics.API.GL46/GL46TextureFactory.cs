// 
// G46TextureFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Graphics.API;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Fluint.Graphics.API.GL46;

public class GL46TextureFactory : ITextureFactory
{
    public void Dispose()
    {
    }

    public ITexture CreateRenderTarget(int width, int height, Format format)
    {
        var data = Array.Empty<byte>();
        var texture = new GL46Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp, ref data);
        return texture;
    }

    public ITexture CreateTexture(int width, int height, Format format, bool createMipMaps = true)
    {
        var data = Array.Empty<byte>();
        var texture = new GL46Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp, ref data);
        return texture;
    }

    public ITexture CreateTexture(int width, int height)
    {
        var texture = new GL46Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp);
        return texture;
    }

    public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
    {
        using var image = Image.Load<Rgba32>(filePath);
        var pixelData = new byte[image.Width * image.Height * 4];
        image.CopyPixelDataTo(pixelData);

        var texture = new GL46Texture(image.Width, image.Height, Filter.Nearest, TextureAddressMode.Clamp,
            ref pixelData);
        return texture;
    }

    public ITexture CreateTexture<T>(int width, int height, Format format, T[] data)
    {
        var texture = new GL46Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp);
        return texture;
    }
}