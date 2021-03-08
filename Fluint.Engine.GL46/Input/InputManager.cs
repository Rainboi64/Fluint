using Fluint.Layer.Graphics;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using GLFW;
using System;

namespace Fluint.Engine.GL46.Input
{
    public class InputManager : IInputManager
    {
        NativeWindow _nativeWindow;

        public event Action<Layer.Input.InputState, Key> Keyboard;
        public event Action<Layer.Input.InputState, Layer.Input.MouseButton> MouseButton;

        private void _nativeWindow_KeyAction(object sender, KeyEventArgs e)
        {
            Keyboard?.Invoke((Layer.Input.InputState)e.State, (Key)e.Key);
        }

        private void _nativeWindow_MouseButton(object sender, MouseButtonEventArgs e)
        {
            MouseButton?.Invoke((Layer.Input.InputState)e.Action, (Layer.Input.MouseButton)e.Button);
        }

        public void Load(IBindingContext bindingContext)
        {
            _nativeWindow = (NativeWindow)bindingContext.NativeContext;
        }

        unsafe Point IInputManager.GetMouseLocation { get => new Point(_nativeWindow.MousePosition.X, _nativeWindow.MousePosition.Y); set => _nativeWindow.MousePosition = new System.Drawing.Point(value.X, value.Y); }

    }
}
