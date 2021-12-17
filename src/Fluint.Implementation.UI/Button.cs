//
// Button.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class Button : IButton
    {
        public string Text { get; set; }
        public string Name { get; private set; }
        public Action OnClick { get; set; }

        public ICollection<IGuiComponent> Children { get; }

        public void Begin(string name)
        {
            Name = name;
        }

        public void Tick()
        {
            if (ImGui.Button($"{Name}###{Text}"))
            {
                OnClick?.Invoke();
            }
        }
    }
}
