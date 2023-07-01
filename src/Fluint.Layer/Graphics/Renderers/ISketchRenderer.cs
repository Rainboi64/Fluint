// 
// ISketchRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.Graphics.Common;

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface ISketchRenderer : IModule, IRenderer
{
    public ModelViewProjection WorldView
    {
        get;
        set;
    }

    void AttachSystem(ISketchSystem system);
}