//
// IGizmoCameraCube.cs
//
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Graphics;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoCameraCube : IGizmo, IModule
{
    ICamera Camera
    {
        get;
        set;
    }
}
