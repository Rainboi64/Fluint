//
// IPolygon.cs
//
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Tools.Sketching.Shapes;

[Initialization(InitializationMethod.Scoped)]
public interface IPolygon : ISketch, IModule
{
    int Segments
    {
        get;
        set;
    }

    float Depth
    {
        get;
        set;
    }

    Vector3 Center
    {
        get;
        set;
    }

    Vector3 Corner
    {
        get;
        set;
    }

    Vector3 Up
    {
        get;
        set;
    }
}
