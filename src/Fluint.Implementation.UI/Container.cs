//
// Container.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class Container : IContainer
    {
        public string Title { get; set; }
        public string Name { get; private set; }
        public ICollection<IGuiComponent> Children { get; }

        public Container()
        {
            Children = new List<IGuiComponent>();
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
