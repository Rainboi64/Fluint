// 
// IGizmoCube.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoCube : IModule, IGizmo
{
    OrientedBoundingBox Box
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