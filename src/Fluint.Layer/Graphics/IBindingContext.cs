//
// IBindingContext.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IBindingContext : IModule, IDisposable
    {
        object NativeContext
        {
            get;
        }

        void InitializeContext(BindingContextSettings settings);
        void Resize(int width, int height);
        void PreRender();
        void PostRender();
        void MakeCurrent();
        void SwapBuffers();
    }
}