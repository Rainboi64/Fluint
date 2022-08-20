//
// Canvas.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

// Move this to the engine folder.

using System;
using System.Collections.Generic;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base
{
    public class Canvas : ICanvas
    {
        private List<int> _editedPixels = new List<int>();

        public Color[] Pixels
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public int ConvertIndex(int x, int y)
        {
            return x + Width * (y - 1);
        }

        public void InitializeCanvas(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new Color[width * height];
        }

        // public ITexture CreateCopyTexture()
        // {
        //     var texture = new Texture(Width, Height);
        //     Array.Copy(Pixels, texture.Pixels, Pixels.Length);
        //     return texture;
        // }
        //
        // public ITexture CreateBoundTexture()
        // {
        //     return new Texture(Width, Height, Pixels);
        // }

        public void Clear()
        {
            foreach (var pixel in _editedPixels)
            {
                Pixels[pixel] = new Color(0);
            }

            _editedPixels.Clear();
        }

        public void Set(int x, int y, Color color)
        {
            if (x > 0 && x < Width && y > 0 && y < Height)
            {
                _editedPixels.Add(ConvertIndex(x, y));
                Pixels[ConvertIndex(x, y)] = color;
            }
        }

        public Color Get(int x, int y)
        {
            return Pixels[ConvertIndex(x, y)];
        }

        public void DrawCircle(Vector2i location, int radius, Color color)
        {
            var x = 0;
            var y = radius;
            var p = 3 - 2 * radius;
            if (radius == 0)
            {
                return;
            }

            while (y >= x)
            {
                Set(location.X - x, location.Y + y, color);
                Set(location.X - y, location.Y + x, color);
                Set(location.X + y, location.Y + x, color);
                Set(location.X + x, location.Y + y, color);

                Set(location.X + x, location.Y - y, color);
                Set(location.X + y, location.Y - x, color);
                Set(location.X - y, location.Y - x, color);
                Set(location.X - x, location.Y - y, color);
                if (p < 0)
                {
                    p += 4 * x++ + 6;
                }
                else
                {
                    p += 4 * (x++ - y--) + 10;
                }
            }
        }

        public void DrawCircle(Vector2i location, int radius, Func<Vector2i, Color> color)
        {
            var x = 0;
            var y = radius;
            var p = 3 - 2 * radius;
            if (radius == 0)
            {
                return;
            }

            while (y >= x)
            {
                Set(location.X - x, location.Y + y, color);
                Set(location.X - y, location.Y + x, color);
                Set(location.X + y, location.Y + x, color);
                Set(location.X + x, location.Y + y, color);
                if (p < 0)
                {
                    p += 4 * x++ + 6;
                }
                else
                {
                    p += 4 * (x++ - y--) + 10;
                }
            }
        }

        public void DrawFilledCircle(Vector2i location, int radius, Color color)
        {
            var x = 0;
            var y = radius;
            var p = 3 - 2 * radius;

            if (radius == 0)
            {
                return;
            }

            while (y >= x)
            {
                // Modified to draw scan-lines instead of edges
                FastDrawLine(color, location.X - x, location.X + x, location.Y - y);
                FastDrawLine(color, location.X - y, location.X + y, location.Y - x);
                FastDrawLine(color, location.X - x, location.X + x, location.Y + y);
                FastDrawLine(color, location.X - y, location.X + y, location.Y + x);
                if (p < 0)
                {
                    p += 4 * x++ + 6;
                }
                else
                {
                    p += 4 * (x++ - y--) + 10;
                }
            }
        }

        public void DrawFilledCircle(Vector2i location, int radius, Func<Vector2i, Color> color)
        {
            var x = 0;
            var y = radius;
            var p = 3 - 2 * radius;

            if (radius == 0)
            {
                return;
            }

            while (y >= x)
            {
                // Modified to draw scan-lines instead of edges
                FastDrawLine(color, location.X - x, location.X + x, location.Y - y);
                FastDrawLine(color, location.X - y, location.X + y, location.Y - x);
                FastDrawLine(color, location.X - x, location.X + x, location.Y + y);
                FastDrawLine(color, location.X - y, location.X + y, location.Y + x);
                if (p < 0)
                {
                    p += 4 * x++ + 6;
                }
                else
                {
                    p += 4 * (x++ - y--) + 10;
                }
            }
        }

        public void DrawFilledQuad(Vector2i v1, Vector2i v2, Vector2i v3, Vector2i v4, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawFilledQuad(Vector2i v1, Vector2i v2, Vector2i v3, Vector2i v4, Func<Vector2i, Color> color)
        {
            throw new NotImplementedException();
        }

        public void DrawFilledRectangle(Vector2i location, Vector2i size, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawFilledRectangle(Vector2i location, Vector2i size, Func<Vector2i, Color> color)
        {
            throw new NotImplementedException();
        }

        public void DrawFilledTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Color color)
        {
            static void Swap(ref int x, ref int y)
            {
                (x, y) = (y, x);
            }

            ;

            int t2X, minx, maxx, t1Xp, t2Xp;

            var changed1 = false;
            var changed2 = false;

            int signx1, signx2;
            int e1;

            // Sort vertices
            if (v1.Y > v2.Y)
            {
                Swap(ref v1.Y, ref v2.Y);
                Swap(ref v1.X, ref v2.X);
            }

            if (v1.Y > v3.Y)
            {
                Swap(ref v1.Y, ref v3.Y);
                Swap(ref v1.X, ref v3.X);
            }

            if (v2.Y > v3.Y)
            {
                Swap(ref v2.Y, ref v3.Y);
                Swap(ref v2.X, ref v3.X);
            }

            var t1X = t2X = v1.X;
            var y = v1.Y;
            var dx1 = v2.X - v1.X;

            if (dx1 < 0)
            {
                dx1 = -dx1;
                signx1 = -1;
            }
            else
            {
                signx1 = 1;
            }

            var dy1 = v2.Y - v1.Y;

            var dx2 = v3.X - v1.X;
            if (dx2 < 0)
            {
                dx2 = -dx2;
                signx2 = -1;
            }
            else
            {
                signx2 = 1;
            }

            var dy2 = v3.Y - v1.Y;

            if (dy1 > dx1)
            {
                // swap values
                Swap(ref dx1, ref dy1);
                changed1 = true;
            }

            if (dy2 > dx2)
            {
                // swap values
                Swap(ref dy2, ref dx2);
                changed2 = true;
            }

            var e2 = dx2 >> 1;
            // Flat top, just process the second half
            if (v1.Y == v2.Y)
            {
                goto next;
            }

            e1 = dx1 >> 1;

            for (var i = 0; i < dx1;)
            {
                t1Xp = 0;
                t2Xp = 0;

                if (t1X < t2X)
                {
                    minx = t1X;
                    maxx = t2X;
                }
                else
                {
                    minx = t2X;
                    maxx = t1X;
                }

                // process first line until y value is about to change
                while (i < dx1)
                {
                    i++;
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1)
                        {
                            t1Xp = signx1; //t1x += signx1;
                        }
                        else
                        {
                            goto next1;
                        }
                    }

                    if (changed1)
                    {
                        break;
                    }
                    else
                    {
                        t1X += signx1;
                    }
                }

                // Move line
                next1:
                // process second line until y value is about to change
                while (true)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2)
                        {
                            t2Xp = signx2; //t2x += signx2;
                        }
                        else
                        {
                            goto next2;
                        }
                    }

                    if (changed2)
                    {
                        break;
                    }
                    else
                    {
                        t2X += signx2;
                    }
                }

                next2:
                if (minx > t1X)
                {
                    minx = t1X;
                }

                if (minx > t2X)
                {
                    minx = t2X;
                }

                if (maxx < t1X)
                {
                    maxx = t1X;
                }

                if (maxx < t2X)
                {
                    maxx = t2X;
                }

                // Draw line from min to max points found on the y
                FastDrawLine(color, minx, maxx, y);
                // Now increase y
                if (!changed1)
                {
                    t1X += signx1;
                }

                t1X += t1Xp;
                if (!changed2)
                {
                    t2X += signx2;
                }

                t2X += t2Xp;
                y += 1;
                if (y == v2.Y)
                {
                    break;
                }
            }

            next:
            // Second half
            dx1 = v3.X - v2.X;
            if (dx1 < 0)
            {
                dx1 = -dx1;
                signx1 = -1;
            }
            else
            {
                signx1 = 1;
            }

            dy1 = v3.Y - v2.Y;
            t1X = v2.X;

            if (dy1 > dx1)
            {
                // swap values
                Swap(ref dy1, ref dx1);
                changed1 = true;
            }
            else
            {
                changed1 = false;
            }

            e1 = dx1 >> 1;

            for (var i = 0; i <= dx1; i++)
            {
                t1Xp = 0;
                t2Xp = 0;
                if (t1X < t2X)
                {
                    minx = t1X;
                    maxx = t2X;
                }
                else
                {
                    minx = t2X;
                    maxx = t1X;
                }

                // process first line until y value is about to change
                while (i < dx1)
                {
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1)
                        {
                            t1Xp = signx1;
                            break;
                        }
                        //t1x += signx1;
                        else
                        {
                            goto next3;
                        }
                    }

                    if (changed1)
                    {
                        break;
                    }
                    else
                    {
                        t1X += signx1;
                    }

                    if (i < dx1)
                    {
                        i++;
                    }
                }

                next3:
                // process second line until y value is about to change
                while (t2X != v3.X)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2)
                        {
                            t2Xp = signx2;
                        }
                        else
                        {
                            goto next4;
                        }
                    }

                    if (changed2)
                    {
                        break;
                    }
                    else
                    {
                        t2X += signx2;
                    }
                }

                next4:

                if (minx > t1X)
                {
                    minx = t1X;
                }

                if (minx > t2X)
                {
                    minx = t2X;
                }

                if (maxx < t1X)
                {
                    maxx = t1X;
                }

                if (maxx < t2X)
                {
                    maxx = t2X;
                }

                FastDrawLine(color, minx, maxx, y);
                if (!changed1)
                {
                    t1X += signx1;
                }

                t1X += t1Xp;
                if (!changed2)
                {
                    t2X += signx2;
                }

                t2X += t2Xp;
                y += 1;
                if (y > v3.Y)
                {
                    return;
                }
            }
        }

        public void DrawFilledTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color)
        {
            static void Swap(ref int x, ref int y)
            {
                (x, y) = (y, x);
            }

            ;

            int t2X, minx, maxx, t1Xp, t2Xp;

            var changed1 = false;
            var changed2 = false;

            int signx1, signx2;
            int e1;

            // Sort vertices
            if (v1.Y > v2.Y)
            {
                Swap(ref v1.Y, ref v2.Y);
                Swap(ref v1.X, ref v2.X);
            }

            if (v1.Y > v3.Y)
            {
                Swap(ref v1.Y, ref v3.Y);
                Swap(ref v1.X, ref v3.X);
            }

            if (v2.Y > v3.Y)
            {
                Swap(ref v2.Y, ref v3.Y);
                Swap(ref v2.X, ref v3.X);
            }

            var t1X = t2X = v1.X;
            var y = v1.Y;
            var dx1 = v2.X - v1.X;

            if (dx1 < 0)
            {
                dx1 = -dx1;
                signx1 = -1;
            }
            else
            {
                signx1 = 1;
            }

            var dy1 = v2.Y - v1.Y;

            var dx2 = v3.X - v1.X;
            if (dx2 < 0)
            {
                dx2 = -dx2;
                signx2 = -1;
            }
            else
            {
                signx2 = 1;
            }

            var dy2 = v3.Y - v1.Y;

            if (dy1 > dx1)
            {
                // swap values
                Swap(ref dx1, ref dy1);
                changed1 = true;
            }

            if (dy2 > dx2)
            {
                // swap values
                Swap(ref dy2, ref dx2);
                changed2 = true;
            }

            var e2 = dx2 >> 1;
            // Flat top, just process the second half
            if (v1.Y == v2.Y)
            {
                goto next;
            }

            e1 = dx1 >> 1;

            for (var i = 0; i < dx1;)
            {
                t1Xp = 0;
                t2Xp = 0;

                if (t1X < t2X)
                {
                    minx = t1X;
                    maxx = t2X;
                }
                else
                {
                    minx = t2X;
                    maxx = t1X;
                }

                // process first line until y value is about to change
                while (i < dx1)
                {
                    i++;
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1)
                        {
                            t1Xp = signx1; //t1x += signx1;
                        }
                        else
                        {
                            goto next1;
                        }
                    }

                    if (changed1)
                    {
                        break;
                    }
                    else
                    {
                        t1X += signx1;
                    }
                }

                // Move line
                next1:
                // process second line until y value is about to change
                while (true)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2)
                        {
                            t2Xp = signx2; //t2x += signx2;
                        }
                        else
                        {
                            goto next2;
                        }
                    }

                    if (changed2)
                    {
                        break;
                    }
                    else
                    {
                        t2X += signx2;
                    }
                }

                next2:
                if (minx > t1X)
                {
                    minx = t1X;
                }

                if (minx > t2X)
                {
                    minx = t2X;
                }

                if (maxx < t1X)
                {
                    maxx = t1X;
                }

                if (maxx < t2X)
                {
                    maxx = t2X;
                }

                // Draw line from min to max points found on the y
                FastDrawLine(color, minx, maxx, y);
                // Now increase y
                if (!changed1)
                {
                    t1X += signx1;
                }

                t1X += t1Xp;
                if (!changed2)
                {
                    t2X += signx2;
                }

                t2X += t2Xp;
                y += 1;
                if (y == v2.Y)
                {
                    break;
                }
            }

            next:
            // Second half
            dx1 = v3.X - v2.X;
            if (dx1 < 0)
            {
                dx1 = -dx1;
                signx1 = -1;
            }
            else
            {
                signx1 = 1;
            }

            dy1 = v3.Y - v2.Y;
            t1X = v2.X;

            if (dy1 > dx1)
            {
                // swap values
                Swap(ref dy1, ref dx1);
                changed1 = true;
            }
            else
            {
                changed1 = false;
            }

            e1 = dx1 >> 1;

            for (var i = 0; i <= dx1; i++)
            {
                t1Xp = 0;
                t2Xp = 0;
                if (t1X < t2X)
                {
                    minx = t1X;
                    maxx = t2X;
                }
                else
                {
                    minx = t2X;
                    maxx = t1X;
                }

                // process first line until y value is about to change
                while (i < dx1)
                {
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1)
                        {
                            t1Xp = signx1;
                            break;
                        }
                        //t1x += signx1;
                        else
                        {
                            goto next3;
                        }
                    }

                    if (changed1)
                    {
                        break;
                    }
                    else
                    {
                        t1X += signx1;
                    }

                    if (i < dx1)
                    {
                        i++;
                    }
                }

                next3:
                // process second line until y value is about to change
                while (t2X != v3.X)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2)
                        {
                            t2Xp = signx2;
                        }
                        else
                        {
                            goto next4;
                        }
                    }

                    if (changed2)
                    {
                        break;
                    }
                    else
                    {
                        t2X += signx2;
                    }
                }

                next4:

                if (minx > t1X)
                {
                    minx = t1X;
                }

                if (minx > t2X)
                {
                    minx = t2X;
                }

                if (maxx < t1X)
                {
                    maxx = t1X;
                }

                if (maxx < t2X)
                {
                    maxx = t2X;
                }

                FastDrawLine(color, minx, maxx, y);
                if (!changed1)
                {
                    t1X += signx1;
                }

                t1X += t1Xp;
                if (!changed2)
                {
                    t2X += signx2;
                }

                t2X += t2Xp;
                y += 1;
                if (y > v3.Y)
                {
                    return;
                }
            }
        }

        public void DrawLine(Vector2i start, Vector2i end, Color color)
        {
            int x, y, xe, ye, i;
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            var dx1 = Math.Abs(dx);
            var dy1 = Math.Abs(dy);
            var px = 2 * dy1 - dx1;
            var py = 2 * dx1 - dy1;

            if (dy1 <= dx1)
            {
                if (dx >= 0)
                {
                    x = start.X;
                    y = start.Y;
                    xe = end.X;
                }
                else
                {
                    x = end.X;
                    y = end.Y;
                    xe = start.X;
                }

                Set(x, y, color);

                for (i = 0; x < xe; i++)
                {
                    x++;
                    if (px < 0)
                    {
                        px += 2 * dy1;
                    }
                    else
                    {
                        y = (dx < 0 && dy < 0) || (dx > 0 && dy > 0) ? y + 1 : y - 1;
                        px += 2 * (dy1 - dx1);
                    }

                    Set(x, y, color);
                }
            }
            else
            {
                if (dy >= 0)
                {
                    x = start.X;
                    y = start.Y;
                    ye = end.Y;
                }
                else
                {
                    x = end.X;
                    y = end.Y;
                    ye = start.Y;
                }

                Set(x, y, color);

                for (i = 0; y < ye; i++)
                {
                    y++;
                    if (py <= 0)
                    {
                        py += 2 * dx1;
                    }
                    else
                    {
                        x = (dx < 0 && dy < 0) || (dx > 0 && dy > 0) ? x + 1 : x - 1;
                        py += 2 * (dx1 - dy1);
                    }

                    Set(x, y, color);
                }
            }
        }

        public void DrawLine(Vector2i start, Vector2i end, Func<Vector2i, Color> color)
        {
            int x, y, xe, ye, i;
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            var dx1 = Math.Abs(dx);
            var dy1 = Math.Abs(dy);
            var px = 2 * dy1 - dx1;
            var py = 2 * dx1 - dy1;

            if (dy1 <= dx1)
            {
                if (dx >= 0)
                {
                    x = start.X;
                    y = start.Y;
                    xe = end.X;
                }
                else
                {
                    x = end.X;
                    y = end.Y;
                    xe = start.X;
                }

                Set(x, y, color);

                for (i = 0; x < xe; i++)
                {
                    x++;
                    if (px < 0)
                    {
                        px += 2 * dy1;
                    }
                    else
                    {
                        y = (dx < 0 && dy < 0) || (dx > 0 && dy > 0) ? y + 1 : y - 1;
                        px += 2 * (dy1 - dx1);
                    }

                    Set(x, y, color);
                }
            }
            else
            {
                if (dy >= 0)
                {
                    x = start.X;
                    y = start.Y;
                    ye = end.Y;
                }
                else
                {
                    x = end.X;
                    y = end.Y;
                    ye = start.Y;
                }

                Set(x, y, color);

                for (i = 0; y < ye; i++)
                {
                    y++;
                    if (py <= 0)
                    {
                        py += 2 * dx1;
                    }
                    else
                    {
                        x = (dx < 0 && dy < 0) || (dx > 0 && dy > 0) ? x + 1 : x - 1;
                        py += 2 * (dx1 - dy1);
                    }

                    Set(x, y, color);
                }
            }
        }

        public void DrawQuad(Vector2i v1, Vector2i v2, Vector2i v3, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawQuad(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Vector2i location, Vector2i size, Color color)
        {
            size.X += location.X;
            size.Y += location.Y;

            for (var i = 0; i < size.X - location.X; i++)
            {
                for (var j = 0; j < size.Y - location.Y; j++)
                {
                    Set(location.X + i, location.Y + j, color);
                }
            }
        }

        public void DrawRectangle(Vector2i location, Vector2i size, Func<Vector2i, Color> color)
        {
            size.X += location.X;
            size.Y += location.Y;

            for (var i = 0; i < size.X - location.X; i++)
            {
                for (var j = 0; j < size.Y - location.Y; j++)
                {
                    Set(location.X + i, location.Y + j, color);
                }
            }
        }

        public void DrawShape(Color color, params Vector2i[] points)
        {
            var length = points.Length;

            for (var i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    DrawLine(points[i], points[0], color);
                    return;
                }

                DrawLine(points[i], points[i + 1], color);
            }
        }

        public void DrawShape(Func<Vector2i, Color> color, params Vector2i[] points)
        {
            var length = points.Length;

            for (var i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    DrawLine(points[i], points[0], color);
                    return;
                }

                DrawLine(points[i], points[i + 1], color);
            }
        }

        public void DrawTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Color color)
        {
            DrawLine(v1, v2, color);
            DrawLine(v2, v3, color);
            DrawLine(v3, v1, color);
        }

        public void DrawTriangle(Vector2i v1, Vector2i v2, Vector2i v3, Func<Vector2i, Color> color)
        {
            DrawLine(v1, v2, color);
            DrawLine(v2, v3, color);
            DrawLine(v3, v1, color);
        }

        private void Set(int x, int y, Func<Vector2i, Color> color)
        {
            if (x > 0 && x < Width && y > 0 && y < Height)
            {
                _editedPixels.Add(ConvertIndex(x, y));
                Pixels[ConvertIndex(x, y)] = color.Invoke(new Vector2i(x, y));
            }
        }

        private void FastDrawLine(Color color, int sx, int ex, int ny)
        {
            for (var i = sx; i <= ex; i++)
            {
                Set(i, ny, color);
            }
        }

        private void FastDrawLine(Func<Vector2i, Color> color, int sx, int ex, int ny)
        {
            for (var i = sx; i <= ex; i++)
            {
                Set(i, ny, color.Invoke(new Vector2i(i, ny)));
            }
        }
    }
}