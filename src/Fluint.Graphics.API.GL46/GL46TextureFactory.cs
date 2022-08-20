// 
// G46TextureFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
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
        throw new NotImplementedException();
    }

    public ITexture CreateTexture(int width, int height, Format format, bool createMipMaps = true)
    {
        throw new NotImplementedException();
    }

    public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
    {
        using var image = Image.Load<Rgba32>(filePath);
        var pixelData = new byte[image.Width * image.Height * 4];
        image.CopyPixelDataTo(pixelData);
        
        var texture = new GL46Texture(image.Width, image.Height, Filter.Nearest, TextureAddressMode.Clamp, ref pixelData);
        return texture;
    }
}