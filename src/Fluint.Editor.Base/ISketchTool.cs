// 
// SketchTool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.Editor;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Graphics.API;

namespace Fluint.Editor.Base;

[Tool("Sketch Tool", "./assets/tools/sketch_tool_48px.png")]
public class SketchTool : ITool, ISketchTool
{
    public string Type
    {
        get;
        set;
    }

    public ICollection<PositionColorVertex> Skeleton
    {
        get;
    }

    public void StartDragging()
    {
        throw new NotImplementedException();
    }

    public void Dragging()
    {
        throw new NotImplementedException();
    }

    public List<PositionColorVertex> StopDragging()
    {
        throw new NotImplementedException();
    }

    public void Begin()
    {
        throw new NotImplementedException();
    }

    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void End()
    {
        throw new NotImplementedException();
    }
}