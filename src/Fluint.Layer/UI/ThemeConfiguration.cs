using System.Numerics;
using Fluint.Layer.Configuration;

namespace Fluint.Layer.UI
{
    [Configuration("Theme", "A Configuration for the theme of Fluint.", "UI")]
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
            TabRounding = 0;
            IndentSpacing = 16.0f;
            ScrollbarSize = 14.0f;
            ScrollbarRounding = 0.0f;
            GrabMinSize = 12.0f;
            GrabRounding = 0.0f;

            Text = new Vector4(0.80f, 0.80f, 0.80f, 1.00f);
            TextDisabled = new Vector4(0.58f, 0.58f, 0.58f, 1.00f);
            WindowBg = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            ChildBg = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            PopupBg = new Vector4(0.25f, 0.25f, 0.25f, 1.00f);
            Border = new Vector4(0.23f, 0.23f, 0.23f, 0.88f);
            BorderShadow = new Vector4(0.92f, 0.91f, 0.88f, 0.00f);
            FrameBg = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            FrameBgHovered = new Vector4(0.31f, 0.31f, 0.31f, 1.00f);
            FrameBgActive = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
            TitleBg = new Vector4(0.19f, 0.19f, 0.19f, 1.00f);
            TitleBgCollapsed = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
            TitleBgActive = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
            MenuBarBg = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            ScrollbarBg = new Vector4(0.17f, 0.17f, 0.17f, 1.00f);
            ScrollbarGrab = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            ScrollbarGrabHovered = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            ScrollbarGrabActive = new Vector4(0.53f, 0.53f, 0.53f, 1.00f);
            CheckMark = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            SliderGrab = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            SliderGrabActive = new Vector4(0.09f, 0.09f, 0.09f, 1.00f);
            Button = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            ButtonHovered = new Vector4(0.32f, 0.32f, 0.32f, 1.00f);
            ButtonActive = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            Header = new Vector4(0.21f, 0.21f, 0.21f, 1.00f);
            HeaderHovered = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            HeaderActive = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            ResizeGrip = new Vector4(0.49f, 0.49f, 0.49f, 0.50f);
            ResizeGripHovered = new Vector4(0.10f, 0.40f, 0.75f, 0.78f);
            ResizeGripActive = new Vector4(0.06f, 0.40f, 0.75f, 1.00f);
            PlotLines = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            PlotLinesHovered = new Vector4(0.56f, 0.56f, 0.58f, 1.00f);
            PlotHistogram = new Vector4(0.06f, 0.05f, 0.07f, 1.00f);
            PlotHistogramHovered = new Vector4(0.18f, 0.35f, 0.58f, 0.86f);
            TextSelectedBg = new Vector4(0.26f, 0.59f, 0.98f, 0.80f);
            Separator = new Vector4(0.34f, 0.34f, 0.34f, 0.50f);

            Success = new Vector4(0.54f, 0.78f, 0.15f, 1f);
            Error = new Vector4(1, 0.35f, 0.36f, 1f);
            Debug = new Vector4(1, 0.8f, 0.22f, 1f);
            Warning = new Vector4(0.54f, 0.78f, 0.15f, 1f);
            Information = new Vector4(0.09f, 0.50f, 0.76f, 1f);
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

        public float TabRounding
        {
            get;
        }

        public Vector4 Separator
        {
            get;
            set;
        }

        public Vector4 Success
        {
            get;
            set;
        }

        public Vector4 Error
        {
            get;
            set;
        }

        public Vector4 Warning
        {
            get;
            set;
        }

        public Vector4 Information
        {
            get;
            set;
        }

        public Vector4 Debug
        {
            get;
            set;
        }
    }
}