using Fluint.Layer.Graphics;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using OpenTK.Windowing.Desktop;
using System;

namespace Fluint.Engine.GL46.Input
{
    public class InputManager : IInputManager
    {
        NativeWindow _nativeWindow;

        public event Action<InputState, Key> Keyboard;
        public event Action<InputState, MouseButton> MouseButton;

        public void Load(IBindingContext bindingContext)
        {
            _nativeWindow = (NativeWindow)bindingContext.NativeContext;
            _nativeWindow.MouseDown += NativeWindow_MouseDown;
            _nativeWindow.MouseUp += NativeWindow_MouseUp;
            _nativeWindow.KeyDown += NativeWindow_KeyDown;
            _nativeWindow.KeyUp += NativeWindow_KeyUp;
        }

        private void NativeWindow_MouseUp(OpenTK.Windowing.Common.MouseButtonEventArgs obj)
        {
            MouseButton?.Invoke(InputState.Release, (MouseButton)obj.Button);
        }

        private void NativeWindow_MouseDown(OpenTK.Windowing.Common.MouseButtonEventArgs obj)
        {
            MouseButton?.Invoke(InputState.Press, (MouseButton)obj.Button);
        }

        private void NativeWindow_KeyUp(OpenTK.Windowing.Common.KeyboardKeyEventArgs obj)
        {
            Keyboard?.Invoke(InputState.Release, (Key)obj.Key);
        }

        private void NativeWindow_KeyDown(OpenTK.Windowing.Common.KeyboardKeyEventArgs obj)
        {
            var newState = InputState.Press;
            if (obj.IsRepeat)
            {
                newState = InputState.Repeat;
            }
            Keyboard?.Invoke(newState, (Key)obj.Key);
        }

        public InputState State(Key key)
        {
            if (_nativeWindow.KeyboardState.IsKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)key))
            {
                if (_nativeWindow.KeyboardState.WasKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)key))
                {
                    return InputState.Repeat;
                }
                return InputState.Press;
            }
            return InputState.Release;
        }

        unsafe Point IInputManager.GetMouseLocation
        {
            get => new((int)_nativeWindow.MousePosition.X, (int)_nativeWindow.MousePosition.Y);
            set => _nativeWindow.MousePosition = new(value.X, value.Y);
        }

    }
}
