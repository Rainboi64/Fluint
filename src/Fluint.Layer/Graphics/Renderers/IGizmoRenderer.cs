// 
// IGizmoRenderer.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.Graphics.Renderers;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmoRenderer : IRenderer, IModule
{
}