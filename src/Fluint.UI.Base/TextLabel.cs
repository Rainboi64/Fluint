//
// TextLabel.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base
{
    public class TextLabel : ITextLabel
    {
        public string Name
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            set;
        }

        public void Begin(string name)
        {
            Name = name;
        }

        public void Tick()
        {
            ImGui.Text(Text);
        }
    }
}