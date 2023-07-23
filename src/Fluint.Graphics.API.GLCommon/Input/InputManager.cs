//
// InputManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

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
    private IWindowProvider _windowProvider;

    public void Load(IWindowProvider windowProvider)
    {
        _windowProvider = windowProvider;
        _nativeKeyboard = (KeyboardState)windowProvider.NativeKeyboardObject;
        _nativeMouse = (MouseState)windowProvider.NativeMouseObject;
    }

    public InputState State(Key key)
    {
        if (!_nativeKeyboard.IsKeyDown((Keys)key))
        {
            return InputState.Release;
        }

        return _nativeKeyboard.WasKeyDown((Keys)key) ? InputState.Repeat : InputState.Press;
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

    public Vector2 MouseScroll => GLExtensions.Vector2(_nativeMouse.Scroll);

    public Vector2 MouseScrollDelta => GLExtensions.Vector2(_nativeMouse.ScrollDelta);

    Vector2 IInputManager.MouseLocation
    {
        get => GLExtensions.Vector2(_nativeMouse.Position);
        set => _windowProvider.SetMouseLocation(value);
    }

    public Vector2 LastMouseLocation => GLExtensions.Vector2(_nativeMouse.PreviousPosition);
    public Vector2 MouseMovementDelta => GLExtensions.Vector2(_nativeMouse.Delta);
}