using System;
using System.Collections.Generic;
using Fluint.Layer.Graphics;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class TextureView : ITextureView
    {
        public string Title { get; set; }
        public string Name { get; private set; }
        public ICollection<IGuiComponent> Children { get; }
        public ITexture Texture { get; set; }
        public TextureView()
        {
        }

        public void Begin(string name)
        {
            Name = name;
        }

        public void Tick()
        {
            if(Texture is not null) 
            {
                ImGui.Begin($"{Name}###{Title}");
                ImGui.Image(new IntPtr(Texture.Handle), new System.Numerics.Vector2(Texture.Size.X, Texture.Size.Y));
                ImGui.End();
            }
        }
    }
}