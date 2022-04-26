//
// TextBox.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
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

        public ICollection<IGuiComponent> Children
        {
            get;
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
}