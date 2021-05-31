using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IWindow : IModule
    {
        public IInputManager InputManager { get; }
        public string Title { get; set; }
        public Vector2i Size { get; set; }
        public Vector2i Location { get; set; }

        public bool VSync { get; set; }
        public void OnLoad();
        public void OnRender(double delay);
        public void OnUpdate(double delay);
        public void OnStart();
        public void OnMouseWheelMoved(Vector2 offset);
        public void OnTextReceived(int unicode, string data);
        public void OnResize(int width, int height);

        public void AdoptGhost<Ghost>() where Ghost : IGhost;

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
