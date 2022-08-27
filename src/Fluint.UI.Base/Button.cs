//
// Button.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base
{
    public class Button : IButton
    {
        public string Text
        {
            get;
            set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Action OnClick
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
            if (ImGui.Button($"{Text}###{Name}"))
            {
                OnClick?.Invoke();
            }
        }
    }
}