// 
// IGizmo.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Editor.Gizmos;

public interface IGizmo
{
    PositionColorVertex[] GenerateVertex();
}