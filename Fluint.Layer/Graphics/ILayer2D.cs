//
// ILayer2D.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//


namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ILayer2D : IModule
    {
        void Render();
    }
}