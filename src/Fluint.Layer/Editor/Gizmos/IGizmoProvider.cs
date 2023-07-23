// 
// IGizmoProvider.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.Editor.Gizmos;

[Initialization(InitializationMethod.Singleton)]
public interface IGizmoProvider : IModule
{
    List<IGizmo> Gizmos
    {
        get;
    }
}