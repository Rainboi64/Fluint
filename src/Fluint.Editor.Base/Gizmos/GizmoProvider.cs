// 
// GizmoProvider.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Editor.Gizmos;

namespace Fluint.Editor.Base.Gizmos;

public class GizmoProvider : IGizmoProvider
{
    public List<IGizmo> Gizmos
    {
        get;
    } = new();
}