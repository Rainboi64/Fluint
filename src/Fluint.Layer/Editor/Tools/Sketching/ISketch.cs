// 
// ISketch.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Tools.Sketching;

[Initialization(InitializationMethod.Scoped)]
public interface ISketch : IModule, IComponent
{
    BoundingBox BoundingBox
    {
        get;
    }

    PositionColorVertex[] Vertex
    {
        get;
        set;
    }

    void Update();
}