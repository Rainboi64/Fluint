// 
// WorldState.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.StateManagement;

namespace Fluint.Layer.Editor;

public class WorldState : IState
{
    public IVertexBuffer HudBuffer;
    public List<PositionColorVertex> WorldVertices = new();
    public IVertexBuffer WorldVerticesBuffer;
}