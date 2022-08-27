// 
// ISketchTool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Editor;

public interface ISketchTool
{
    ICollection<PositionColorVertex> Skeleton
    {
        get;
    }

    public void StartDragging();
    public void Dragging();
    public List<PositionColorVertex> StopDragging();
}