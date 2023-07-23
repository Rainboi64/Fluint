// 
// Viewport.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;

namespace Fluint.UI.Base;

public class Viewport : IViewport
{
    private Vector2i _size;

    public Viewport()
    {
        _size = new Vector2i(750, 750);
    }

    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
        ImGui.Image(new IntPtr(SwapChain.TextureView.Handle), new Vector2(Size.X, Size.Y));
    }

    public ISwapChain SwapChain
    {
        get;
        set;
    }

    public Vector2i Size
    {
        get => _size;
        set
        {
            _size = value;
            SwapChain.Modify(new SwapChainDescriptor(_size.X, _size.Y, false, false, SwapEffect.Discard));
        }
    }
}