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

namespace Fluint.Engine.GL46.Input
{
    public class InputManager : IInputManager
    {
        KeyboardState _nativeKeyboard;
        MouseState _nativeMouse;

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
            return _nativeKeyboard.IsKeyPressed((Keys)key);
        }

        public bool IsKeyReleased(Key key)
        {
            return _nativeKeyboard.IsKeyReleased((Keys)key);
        }

        public bool WasKeyPressed(Key key)
        {
            return _nativeKeyboard.WasKeyDown((Keys)key);
        }

        public InputState State(Layer.Input.MouseButton button)
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

        public bool IsMouseButtonPressed(Layer.Input.MouseButton button)
        {
            return _nativeMouse.IsButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }

        public bool IsMouseButtonReleased(Layer.Input.MouseButton button)
        {
            return !_nativeMouse.WasButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }

        public bool WasMouseButtonPressed(Layer.Input.MouseButton button)
        {
            return _nativeMouse.WasButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }

        unsafe Vector2 IInputManager.MouseLocation
        {
            get => OpenTKHelper.Vector2(_nativeMouse.Position);
            set => throw new NotImplementedException();
        }
        public Vector2 LastMouseLocation { get => OpenTKHelper.Vector2(_nativeMouse.PreviousPosition); }
        public Vector2 MouseMovementDelta { get => OpenTKHelper.Vector2(_nativeMouse.Delta); }
    }
}
