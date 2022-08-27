// 
// IRenderer.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics;

public interface IRenderer
{
    ICommandList CommandList
    {
        get;
    }

    public void Start();
    public void Push();
    public void End();
}