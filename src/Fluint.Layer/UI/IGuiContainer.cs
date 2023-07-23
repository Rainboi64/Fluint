// 
// IGuiContainer.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.UI;

public interface IGuiContainer<T> : IGuiComponent, IDictionary<string, T> where T : IGuiComponent
{
}