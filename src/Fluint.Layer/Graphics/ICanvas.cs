//
// ICanvas.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICanvas : IModule
    {
        Color[] Pixels
        {
            get;
        }

        int Width
        {
            get;
        }

        int Height
        {
            get;
        }

        void InitializeCanvas(int width, int height);

        int ConvertIndex(int x, int y);

        void Clear();
        void Clear(Color color);
        void Set(int x, int y, Color color);
        Color Get(int x, int y);

        void DrawShape(Color color, params Vector2i[] points);
        void DrawShape(Func<Vector2i, Color> color, params Vector2i[] points);

        void DrawLine(Vector2i start, Vector2i end, Color color);
        void DrawLine(Vector2i start, Vector2i end, Func<Vector2i, Color> color);

        void DrawTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Color color);
        void DrawTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color);

        void DrawFilledTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Color color);
        void DrawFilledTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color);

        void DrawRectangle(Vector2i location, Vector2i size, Color color);
        void DrawRectangle(Vector2i location, Vector2i size, Func<Vector2i, Color> color);

        void DrawFilledRectangle(Vector2i location, Vector2i size, Color color);
        void DrawFilledRectangle(Vector2i location, Vector2i size, Func<Vector2i, Color> color);

        void DrawQuad(Vector2i v1, Vector2i v2, Vector2i v3, Color color);
        void DrawQuad(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color);

        void DrawFilledQuad(Vector2i v1, Vector2i v2, Vector2i v3, Vector2i v4, Color color);
        void DrawFilledQuad(Vector2i v1, Vector2i v2, Vector2i v3, Vector2i v4, Func<Vector2i, Color> color);

        void DrawCircle(Vector2i location, int radius, Color color);
        void DrawCircle(Vector2i location, int radius, Func<Vector2i, Color> color);

        void DrawFilledCircle(Vector2i location, int radius, Color color);
        void DrawFilledCircle(Vector2i location, int radius, Func<Vector2i, Color> color);
    }
}