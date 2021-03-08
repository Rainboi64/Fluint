using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IBindingContext : IModule, IDisposable
    {
        void InitializeContext(BindingContextSettings settings);
        void Resize(int Width, int Height);
        void PreRender();
        void PostRender();
        void MakeCurrent();
        void SwapBuffers();
        object NativeContext { get; }
    }
}
