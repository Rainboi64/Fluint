// 
// G46TextureFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.IO;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = Fluint.Layer.Mathematics.Color;

namespace Fluint.Graphics.API.GL33;

public class GL33TextureFactory : ITextureFactory
{
    private readonly ILogger _logger;

    public GL33TextureFactory(ILogger logger)
    {
        _logger = logger;
    }
    
    public void Dispose()
    {
    }

    public ITexture CreateRenderTarget(int width, int height, Format format)
    {
        var data = Array.Empty<byte>();
        var texture = new GL33Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp, ref data);
        return texture;
    }

    public ITexture CreateTexture(int width, int height, Format format, bool createMipMaps = true)
    {
        var data = Array.Empty<byte>();
        var texture = new GL33Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp, ref data);
        return texture;
    }

    public ITexture CreateTexture(int width, int height)
    {
        var texture = new GL33Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp);
        return texture;
    }

    public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
    {
        if (File.Exists(filePath))
        {
            using var image = Image.Load<Rgba32>(filePath);
            var pixelData = new byte[image.Width * image.Height * 4];
            image.CopyPixelDataTo(pixelData);
            var texture = new GL33Texture(image.Width, image.Height, Filter.Nearest, TextureAddressMode.Clamp,
                ref pixelData);
            return texture;
        }
        else
        {
            _logger.Error("Texture not found {0}", filePath);
            int size = 256; // Size of the texture
            int cellSize = 50; // Size of each cell in the checkerboard
            var colors = new byte[size * size * 4];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    int index = (y * size + x) * 4;
                    bool isWhite = ((x / cellSize + y / cellSize) % 2) == 0;
                    byte color = isWhite ? (byte)255 : (byte)0;
                    colors[index] = color; // R
                    colors[index + 1] = color; // G
                    colors[index + 2] = color; // B
                    colors[index + 3] = 255; // A
                }
            }
            
            var texture = new GL33Texture(256, 256, Filter.Nearest, TextureAddressMode.Clamp,
                ref colors);
            return texture;
        }
    }

    public ITexture CreateTexture<T>(int width, int height, Format format, T[] data)
    {
        var texture = new GL33Texture(width, height, Filter.Nearest, TextureAddressMode.Clamp);
        return texture;
    }
}