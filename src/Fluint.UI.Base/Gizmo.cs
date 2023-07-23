// 
// Gizmo.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.UI;

namespace Fluint.UI.Base;

public class Gizmo : IGizmo
{
    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
    }
}