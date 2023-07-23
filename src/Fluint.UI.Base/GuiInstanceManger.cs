//
// GuiInstanceManger.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.UI.Base;

public class GuiInstanceManger : IGuiInstanceManager
{
    private readonly List<IWindow> _windows = new();
    public IReadOnlyCollection<IWindow> Windows => _windows;

    public void Adopt(in IWindow window)
    {
        _windows.Add(window);
    }
}