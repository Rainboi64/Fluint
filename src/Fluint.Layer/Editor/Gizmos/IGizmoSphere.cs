// 
// IGizmoSphere.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoSphere : IModule, IGizmo
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

    int LongitudeLines
    {
        get;
        set;
    }

    int LatitudeLines
    {
        get;
        set;
    }

    Color Color
    {
        get;
        set;
    }
}