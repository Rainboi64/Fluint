// 
// ILayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Windowing;

namespace Fluint.Layer.UI;

public interface ILayout 
{
    void Initialize(IUILayer layer);
}