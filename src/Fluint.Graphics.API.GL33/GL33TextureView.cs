// 
// TextureView.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Graphics.API.GL33;

internal class GL33TextureView : TextureView
{
    public GL33TextureView(GL33Texture gl33Texture)
    {
        Handle = gl33Texture;
    }

    public static implicit operator int(GL33TextureView textureView)
    {
        return textureView.Handle;
    }
}