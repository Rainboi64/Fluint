// 
// TextureView.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Graphics.API.GL46;

internal class GL46TextureView : TextureView
{
    public GL46TextureView(GL46Texture gl46Texture)
    {
        Handle = gl46Texture;
    }

    public static implicit operator int(GL46TextureView textureView)
    {
        return textureView.Handle;
    }
}