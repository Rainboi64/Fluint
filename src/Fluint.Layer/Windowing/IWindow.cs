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

        public ICollection<IGuiComponent> Controls
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

        public bool VSync
        {
            get;
            set;
        }

        public void OnLoad();
        public void OnRender(double delay);
        public void OnUpdate(double delay);
        public void OnStart();
        public void OnMouseWheelMoved(Vector2 offset);
        public void OnTextReceived(int unicode, string data);
        public void OnResize(int width, int height);

        public void AdoptGhost<TGhost>() where TGhost : IGhost;

        /// <summary>
        /// to be used to render on frames.
        /// </summary>
        /// <param name="action"></param>
        public void Enqueue(Action action);

        /// <summary>
        /// Don't call outside provider
        /// </summary>
        public void SetProvider(in IWindowProvider provider);
    }
}