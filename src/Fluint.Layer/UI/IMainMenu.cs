// 
// IMainMenu.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IMainMenu : IModule, IGuiContainer<IMenuItem>
{
}