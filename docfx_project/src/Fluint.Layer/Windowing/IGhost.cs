//
// IGhost.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing
{
    public interface IGhost
    {
        /// <summary>
        /// Don't call outside IWindow
        /// </summary>
        public void SetPossessed(in IWindow possessor) { }

        public void OnLoad() { }
        public void OnRender(double delay) { }
        public void OnUpdate(double delay) { }
        public void OnMouseWheelMoved(Vector2 offset);
        public void OnStart() { }
        public void OnTextReceived(int unicode, string data) { }
        public void OnResize(int width, int height) { }
    }
}
