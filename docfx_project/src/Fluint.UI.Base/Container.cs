//
// Container.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.SDK.Base.UI
{
    public class Container : IContainer
    {
        public Container()
        {
            Children = new List<IGuiComponent>();
        }

        public string Title
        {
            get;
            set;
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

        public void Begin(string name)
        {
            Name = name;
        }

        public void Tick()
        {
            ImGui.Begin($"{Name}###{Title}");

            foreach (var item in Children)
            {
                item.Tick();
            }

            ImGui.End();
        }
    }
}