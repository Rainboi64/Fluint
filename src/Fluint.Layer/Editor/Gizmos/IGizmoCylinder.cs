// 
// IGizmoCone.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoCylinder : IModule, IGizmo
{
    bool IsCone
    {
        get;
        set;
    }

    Vector3 Center
    {
        get;
        set;
    }

    int BaseSegments
    {
        get;
        set;
    }

    float Height
    {
        get;
        set;
    }

    float Radius
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