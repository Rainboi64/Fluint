// 
// TextureView.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Engine.GL46.Graphics;

internal class GL46TextureView : TextureView
{
    private readonly int _nativeTexture;

    public GL46TextureView(Texture texture)
    {
        _nativeTexture = texture;
    }

    public static implicit operator int(GL46TextureView textureView)
    {
        return textureView._nativeTexture;
    }
}