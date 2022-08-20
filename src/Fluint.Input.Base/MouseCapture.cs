// 
// MouseCapture.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Windowing;

namespace Fluint.Input.Base;

public class MouseCapture : IMouseCapture
{
    public int X => (int)Math.Ceiling(_x);
    public int Y => (int)Math.Ceiling(_y);

    private float _x;
    private float _y;

    private const int Trigger = 2;
    private const int Padding = 2;

    private IWindow _window;
    private Vector2i _screenSize;
    private Vector2 _windowLocation;
    private Vector2 _lastMousePosition = Vector2.Zero;
    private Vector2 _delta;
    private Vector2 _mouseLocation;
    private IInputManager _inputManager;

    public void Update()
    {
        _windowLocation = new Vector2(_window.Location.X, _window.Location.Y);
        _mouseLocation = _windowLocation + _window.InputManager.MouseLocation;

        _delta = _lastMousePosition - _mouseLocation;
        _lastMousePosition = _mouseLocation;
    }

    public void Begin(IWindow window)
    {
        _window = window;
        _inputManager = _window.InputManager;
    }

    public void Capture()
    {
        _screenSize = _window.ScreenSize;

        if (_screenSize.X - _mouseLocation.X <= Trigger)
        {
            _inputManager.MouseLocation = new Vector2(Padding - _windowLocation.X, _inputManager.MouseLocation.Y);
            _delta.X -= _screenSize.X;
        }
        else if (_mouseLocation.X <= Trigger)
        {
            _inputManager.MouseLocation = new Vector2(_screenSize.X - Padding, _inputManager.MouseLocation.Y);
            _delta.X += _screenSize.X;
        }

        if (_screenSize.Y - _mouseLocation.Y < Trigger)
        {
            _inputManager.MouseLocation = new Vector2(_inputManager.MouseLocation.X, -_windowLocation.Y);
            _delta.Y -= _screenSize.Y;
        }
        else if (_mouseLocation.Y < Trigger)
        {
            _inputManager.MouseLocation = new Vector2(_inputManager.MouseLocation.X, _screenSize.Y - Padding);
            _delta.Y += _screenSize.Y;
        }

        _x += _delta.X;
        _y += _delta.Y;
    }
}