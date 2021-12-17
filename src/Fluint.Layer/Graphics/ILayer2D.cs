//
// ILayer2D.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//


namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ILayer2D : IModule
    {
        void Render();
    }
}