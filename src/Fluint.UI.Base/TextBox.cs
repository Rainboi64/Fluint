//
// TextBox.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class TextBox : ITextBox
{
    private string _text;

    public bool IsMultiLine
    {
        get;
        set;
    }

    public string Text
    {
        get => _text;
        set => _text = value;
    }

    public string Name
    {
        get;
        private set;
    }

    public string SideText
    {
        get;
        set;
    }

    public void Begin(string name)
    {
        Name = name;
        Text = string.Empty;
        SideText = string.Empty;
    }

    public void Tick()
    {
        ImGui.InputText(SideText, ref _text, 512);
    }
}