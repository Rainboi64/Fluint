// 
// CameraToolState.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Input;
using Fluint.Layer.StateManagement;

namespace Fluint.Editor.Base.Tools;

public class CameraToolState : IState
{
    public readonly IMouseCapture MouseCapture;

    public CameraToolState(IMouseCapture mouseCapture)
    {
        MouseCapture = mouseCapture;
    }
}