// 
// ISketch.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Tools.Sketching;

public interface ISketch : IComponent
{
    BoundingBox BoundingBox
    {
        get;
    }

    Vector3[] Vertices
    {
        get;
    }
}