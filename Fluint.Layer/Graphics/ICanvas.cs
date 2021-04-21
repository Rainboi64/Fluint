using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    public interface ICanvas
    {
        public Color[] Pixels { get; }

        public int Width { get; }
        public int Height { get; }

        public int ConvertIndex(int x, int y);
        public void Render();

        public void DrawShape(Color color, params Vector2i[] points);
        public void DrawShape(Func<Vector2i, Color> color, params Vector2i[] points);

        public void DrawLine(Vector2i start, Vector2i end, Color color);
        public void DrawLine(Vector2i start, Vector2i end, Func<Vector2i, Color> color);

        public void DrawTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Color color);
        public void DrawTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color);

        public void DrawFilledTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Color color);
        public void DrawFilledTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color);

        public void DrawRectangle(Vector2i location, Vector2i size, Color color);
        public void DrawRectangle(Vector2i location, Vector2i size, Func<Vector2i, Color> color);

        public void DrawFilledRectangle(Vector2i location, Vector2i size, Color color);
        public void DrawFilledRectangle(Vector2i location, Vector2i size, Func<Vector2i, Color> color);

        public void DrawQuad(Vector2i v1, Vector2i v2, Vector2i v3, Color color);
        public void DrawQuad(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color);

        public void DrawFilledQuad(Vector2i v1, Vector2i v2, Vector2i v3, Vector2i v4, Color color);
        public void DrawFilledQuad(Vector2i v1, Vector2i v2, Vector2i v3, Vector2i v4, Func<Vector2i, Color> color);

        public void DrawCircle(Vector2i location, float radius, Color color);
        public void DrawCircle(Vector2i location, float radius, Func<Vector2i, Color> color);

        public void DrawFilledCircle(Vector2i location, float radius, Color color);
        public void DrawFilledCircle(Vector2i location, float radius, Func<Vector2i, Color> color);
    }
}
