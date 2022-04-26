//
// TextLabel.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class TextLabel : ITextLabel
    {
        public string Name
        {
            get;
            private set;
        }

        public ICollection<IGuiComponent> Children
        {
            get;
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