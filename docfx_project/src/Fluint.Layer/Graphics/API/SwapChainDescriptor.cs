// 
// SwapChainDescriptor.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Graphics.API;

public class SwapChainDescriptor
{
    public readonly int Height;
    public readonly SwapEffect SwapEffect;
    public readonly int Width;

    public SwapChainDescriptor(int width, int height, bool isWindowed, bool vSync, SwapEffect swapEffect)
    {
        Width = width;
        Height = height;
        SwapEffect = swapEffect;
    }
}