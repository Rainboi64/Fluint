//
// IWindow.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;

namespace Fluint.Layer.Windowing
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IWindow : IModule
    {
        public IInputManager InputManager
        {
            get;
        }

        public IDictionary<string, IGuiComponent> Controls
        {
            get;
        }

        public double FrameTime
        {
            get;
        }

        public string Title
        {
            get;
            set;
        }

        public Vector2i Size
        {
            get;
            set;
        }

        public Vector2i Location
        {
            get;
            set;
        }

        public Vector2i ScreenSize
        {
            get;
        }

        public bool VSync
        {
            get;
            set;
        }

        T SpawnControl<T>() where T : Control;


        public event EventHandler Load;
        public event EventHandler<RenderEvent> Render;
        public event EventHandler<RenderEvent> Update;
        public event EventHandler<ResizeEvent> Resize;


        public void Close();
        public void OnLoad();
        public void OnRender(double delay);
        public void OnUpdate(double delay);
        public void OnStart();
        public void OnMouseWheelMoved(Vector2 offset);
        public void OnTextReceived(int unicode, string data);
        public void OnResize(int width, int height);

        public void Puppet<TPuppet>() where TPuppet : IPuppet;
        public void Enqueue(Action action);
        public void SetProvider(in IWindowProvider provider);
    }
}