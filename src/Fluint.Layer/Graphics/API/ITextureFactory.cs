// 
// ITextureFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Graphics.API;

public interface ITextureFactory
{
    ITexture CreateRenderTarget(int width, int height, Format format);

    ITexture CreateTexture(int width, int height, Format format, bool createMipMaps = true);

    ITexture CreateTextureFromFile(string filePath, bool createMipMaps);
}