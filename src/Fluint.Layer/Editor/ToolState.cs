// 
// ToolState.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Editor.Tools;
using Fluint.Layer.StateManagement;

namespace Fluint.Layer.Editor;

public class ToolState : IState
{
    public ITool ActiveTool
    {
        get;
        set;
    }
}