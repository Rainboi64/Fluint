//
// IRenderer2D.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//


namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IRenderer2D : IModule
    {
        void Begin();
        void Submit(IRenderable2D renderable2D);
        void Flush();
        void End();
    }
}