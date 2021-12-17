//
// FontManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class FontManager : IFontManager
    {
        public Font LoadFont(string fontFileName, float fontSize = 16)
        {
            var io = ImGui.GetIO();
            var font = io.Fonts.AddFontFromFileTTF(fontFileName, fontSize);
            return new Font(() => { ImGui.PushFont(font); }, () => { ImGui.PopFont(); });
        }
    }
}
