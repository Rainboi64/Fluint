// 
// IGrabCircle.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoCircularPolygon : IModule, IGizmo
{
    Vector3 Center
    {
        get;
        set;
    }

    float Radius
    {
        get;
        set;
    }

    int Sides
    {
        get;
        set;
    }
}