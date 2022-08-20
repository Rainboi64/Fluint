// 
// ISwapChain.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.Graphics.API;

public interface ISwapChain : IDisposable
{
    TextureView TextureView
    {
        get;
    }

    TextureView DepthStencilView
    {
        get;
    }

    void Present();
    void Disconnect();
    void Modify(SwapChainDescriptor descriptor);
}