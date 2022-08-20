//
// InputManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Windowing;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MouseButton = Fluint.Layer.Input.MouseButton;

namespace Fluint.Graphics.API.GLCommon.Input;

public class InputManager : IInputManager
{
    private KeyboardState _nativeKeyboard;
    private MouseState _nativeMouse;

    public void Load(IWindowProvider windowProvider)
    {
        _nativeKeyboard = (KeyboardState)windowProvider.NativeKeyboardObject;
        _nativeMouse = (MouseState)windowProvider.NativeMouseObject;
    }

    public InputState State(Key key)
    {
        if (_nativeKeyboard.IsKeyDown((Keys)key))
        {
            if (_nativeKeyboard.WasKeyDown((Keys)key))
            {
                return InputState.Repeat;
            }

            return InputState.Press;
        }

        return InputState.Release;
    }

    public bool IsKeyPressed(Key key)
    {
        return _nativeKeyboard.IsKeyDown((Keys)key);
    }

    public bool IsKeyReleased(Key key)
    {
        return _nativeKeyboard.IsKeyReleased((Keys)key);
    }

    public bool WasKeyPressed(Key key)
    {
        return _nativeKeyboard.WasKeyDown((Keys)key);
    }

    public InputState State(MouseButton button)
    {
        if (_nativeMouse.IsButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button))
        {
            if (_nativeMouse.WasButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button))
            {
                return InputState.Repeat;
            }

            return InputState.Press;
        }

        return InputState.Release;
    }

    public bool IsMouseButtonPressed(MouseButton button)
    {
        return _nativeMouse.IsButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
    }

    public bool IsMouseButtonReleased(MouseButton button)
    {
        return !_nativeMouse.WasButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
    }

    public bool WasMouseButtonPressed(MouseButton button)
    {
        return _nativeMouse.WasButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
    }

    Vector2 IInputManager.MouseLocation
    {
        get => OpenTkHelper.Vector2(_nativeMouse.Position);
        set => throw new NotImplementedException();
    }

    public Vector2 LastMouseLocation => OpenTkHelper.Vector2(_nativeMouse.PreviousPosition);
    public Vector2 MouseMovementDelta => OpenTkHelper.Vector2(_nativeMouse.Delta);
}