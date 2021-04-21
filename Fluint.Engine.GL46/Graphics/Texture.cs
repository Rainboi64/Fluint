//
// Texture.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using Fluint.Layer.Graphics;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Fluint.Engine.GL46.Graphics
{
    public class Texture : ITexture
    {
        public string Filename { get; private set; }
        public int Handle { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Layer.Mathematics.Color[] Pixels { get; private set; }

        public Texture(int width, int height)
        {
            Width = width;
            Height = height;

            Handle = GL.GenTexture();

            Bind();
            SetupTextureFilters();
            Unbind();
        }

        private static void SetupTextureFilters()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public Texture(string filename)
        {
            LoadFromFile(filename);
        }

        public void Bind() 
        {
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void LoadFromFile(string fileName)
        {
            Filename = fileName;
            Handle = GL.GenTexture();
            var img = (Image<Rgba32>)Image.Load(Filename);

            Bind();

            SetupTextureFilters();

            Width = img.Width;
            Height = img.Height;

            Pixels = new Layer.Mathematics.Color[Width * Height];

            img.Mutate(x => x.Flip(FlipMode.Vertical));

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    // I could use some bit-hack magicary in here.
                    // It would also be wise to use System.Drawing.
                    Pixels[ConvertIndex(x, y)] = new Layer.Mathematics.Color(
                        img[x, y].R,
                        img[x, y].G,
                        img[x, y].B);
                }
            }
            Upload();
        }

        //Disposing Code

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                Unbind();
                GL.DeleteTexture(Handle);

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int ConvertIndex(int x, int y)
        {
            return x + Width * y;
        }

        public void Upload()
        {
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                Width,
                Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                Pixels);
        }
    }
}
