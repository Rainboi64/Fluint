using System.Numerics;
using Fluint.Layer.Configuration;

namespace Fluint.SDK.Base.UI
{
    [Configuration("Themes")]
    public class ThemeConfiguration : IConfiguration
    {
        public ThemeConfiguration()
        {
            WindowPadding = new Vector2(6, 6);
            WindowRounding = 0.0f;
            FramePadding = new Vector2(4, 4);
            FrameRounding = 0.0f;
            ItemSpacing = new Vector2(8, 8);
            ItemInnerSpacing = new Vector2(4, 4);
            IndentSpacing = 16.0f;
            ScrollbarSize = 16.0f;
            ScrollbarRounding = 0.0f;
            GrabMinSize = 12.0f;
            GrabRounding = 0.0f;

            Text = new Vector4(0.80f, 0.80f, 0.83f, 1.00f);
            TextDisabled = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            WindowBg = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            ChildBg = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
            PopupBg = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
            Border = new Vector4(0.20f, 0.20f, 0.23f, 0.88f);
            BorderShadow = new Vector4(0.92f, 0.91f, 0.88f, 0.00f);
            FrameBg = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            FrameBgHovered = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            FrameBgActive = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            TitleBg = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            TitleBgCollapsed = new Vector4(1.00f, 0.98f, 0.95f, 0.75f);
            TitleBgActive = new Vector4(0.07f, 0.07f, 0.09f, 1.00f);
            MenuBarBg = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            ScrollbarBg = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            ScrollbarGrab = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            ScrollbarGrabHovered = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            ScrollbarGrabActive = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            CheckMark = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            SliderGrab = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            SliderGrabActive = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            Button = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            ButtonHovered = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);
            ButtonActive = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            Header = new Vector4(0.10f, 0.09f, 0.12f, 1.00f);
            HeaderHovered = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            HeaderActive = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            ResizeGrip = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            ResizeGripHovered = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            ResizeGripActive = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            PlotLines = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
            PlotLinesHovered = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
            PlotHistogram = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
            PlotHistogramHovered = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
            TextSelectedBg = new Vector4(0.25f, 1.00f, 0.00f, 0.43f);
        }

        public float FrameRounding
        {
            get;
        }

        public float IndentSpacing
        {
            get;
        }

        public float ScrollbarSize
        {
            get;
        }

        public float ScrollbarRounding
        {
            get;
        }

        public float GrabMinSize
        {
            get;
        }

        public float GrabRounding
        {
            get;
        }

        public float WindowRounding
        {
            get;
        }

        public Vector2 WindowPadding
        {
            get;
        }

        public Vector2 FramePadding
        {
            get;
        }

        public Vector2 ItemSpacing
        {
            get;
        }

        public Vector2 ItemInnerSpacing
        {
            get;
        }

        public Vector4 Text
        {
            get;
        }

        public Vector4 TextDisabled
        {
            get;
        }

        public Vector4 WindowBg
        {
            get;
        }

        public Vector4 ChildBg
        {
            get;
        }

        public Vector4 PopupBg
        {
            get;
        }

        public Vector4 Border
        {
            get;
        }

        public Vector4 BorderShadow
        {
            get;
        }

        public Vector4 FrameBg
        {
            get;
        }

        public Vector4 FrameBgHovered
        {
            get;
        }

        public Vector4 FrameBgActive
        {
            get;
        }

        public Vector4 TitleBg
        {
            get;
        }

        public Vector4 TitleBgCollapsed
        {
            get;
        }

        public Vector4 TitleBgActive
        {
            get;
        }

        public Vector4 MenuBarBg
        {
            get;
        }

        public Vector4 ScrollbarBg
        {
            get;
        }

        public Vector4 ScrollbarGrab
        {
            get;
        }

        public Vector4 ScrollbarGrabHovered
        {
            get;
        }

        public Vector4 ScrollbarGrabActive
        {
            get;
        }

        public Vector4 CheckMark
        {
            get;
        }

        public Vector4 SliderGrab
        {
            get;
        }

        public Vector4 SliderGrabActive
        {
            get;
        }

        public Vector4 Button
        {
            get;
        }

        public Vector4 ButtonHovered
        {
            get;
        }

        public Vector4 ButtonActive
        {
            get;
        }

        public Vector4 Header
        {
            get;
        }

        public Vector4 HeaderHovered
        {
            get;
        }

        public Vector4 HeaderActive
        {
            get;
        }

        public Vector4 ResizeGrip
        {
            get;
        }

        public Vector4 ResizeGripHovered
        {
            get;
        }

        public Vector4 ResizeGripActive
        {
            get;
        }

        public Vector4 PlotLines
        {
            get;
        }

        public Vector4 PlotLinesHovered
        {
            get;
        }

        public Vector4 PlotHistogram
        {
            get;
        }

        public Vector4 PlotHistogramHovered
        {
            get;
        }

        public Vector4 TextSelectedBg
        {
            get;
        }
    }
}