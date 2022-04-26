using System;
using System.Collections.Generic;
using System.Numerics;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Graphics;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class TextureView : ITextureView
    {
        private readonly ILogger _logger;

        public TextureView(ILogger logger)
        {
            _logger = logger;
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

        public ITexture Texture
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
            if (Texture is not null)
            {
                ImGui.Begin($"{Name}###{Title}");
                ImGui.Image(new IntPtr(Texture.Handle), new Vector2(Texture.Size.X, Texture.Size.Y));
                ImGui.End();

                return;
            }

            _logger.Warning("[{0}] Texture is null", "TextureView");
        }
    }
}