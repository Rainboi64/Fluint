using Fluint.Layer.Configuration;

namespace Fluint.Engine.GL46.ImGuiImpl
{
    [Configuration("Window Configuration", "A Configuration for the OpenGL46 window provider.", "OpenGL46")]
    public class ImGuiConfiguration : IConfiguration
    {
        public ImGuiConfiguration()
        {
            Fonts = new[] { new Font("./fonts/default.ttf", 16.0f) };
        }

        public Font[] Fonts
        {
            get;
            set;
        }

        public readonly record struct Font(string FontPath, float FontSize);
    }
}