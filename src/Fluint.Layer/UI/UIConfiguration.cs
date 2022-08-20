using Fluint.Layer.Configuration;

namespace Fluint.Layer.UI
{
    [Configuration("UI Configuration", "A Configuration for the OpenGL46 window provider.", "UI")]
    public class UIConfiguration : IConfiguration
    {
        public UIConfiguration()
        {
            Fonts = new[] {
                new Font("./fonts/Roboto.ttf", 16.0f),
                new Font("./fonts/OpenSans.ttf", 18.0f),
                new Font("./fonts/SpaceGrotesk.ttf", 18.0f),
            };
            Language = "en_us";
        }

        public Font[] Fonts
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public readonly record struct Font(string FontPath, float FontSize);
    }
}