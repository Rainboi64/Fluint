// 
// Demo.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class Demo : IDemo
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
        ImGui.ShowDemoWindow();
    }
}