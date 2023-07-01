// 
// SketchTool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Editor.Viewport;

namespace Fluint.Editor.Base.Tools.Sketching;

[Tool("Sketch Tool", "./assets/tools/sketch_tool_48px.png")]
public class SketchTool : ITool
{
    public void Begin(IViewportContext context)
    {
        context.OnTick += ViewportOnOnTick;
    }

    private void ViewportOnOnTick(object sender, EventArgs e)
    {
    }
}