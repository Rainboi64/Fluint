// 
// IGizmo.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IGizmo : IModule, IGuiComponent
{
}