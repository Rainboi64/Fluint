// 
// IGizmoLine.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoLine : IModule, IGizmo
{
    Vector3 Start
    {
        get;
        set;
    }

    Vector3 End
    {
        get;
        set;
    }
}