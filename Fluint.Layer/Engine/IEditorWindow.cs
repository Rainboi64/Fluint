using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    public interface IEditorWindow
    {
        ICamera Camera { get; }
        void Load();
        void Update();
        void Render();
    }
}
