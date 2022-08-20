// 
// IControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.UI;

public class Control : IGuiComponent
{
    protected readonly List<IGuiComponent> Children = new();
    public string Name
    {
        get;
        private set;
    }

    public virtual void Begin(string name, IWindow parent)
    {
        Name = name;
        foreach (var guiComponent in Children)
        {
            guiComponent.Begin(Name + guiComponent.Name);
        }
    }

    public void Begin(string name)
    {
        Begin(name, null);
    }

    public virtual void Tick()
    {
        foreach (var guiComponent in Children)
        {
            guiComponent.Tick();
        }
    }
}