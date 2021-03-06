//
// Texture.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Graphics;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Fluint.Engine.GL46.Graphics
{
    public class Texture : ITexture
    {
        public static void ConfigureTextures()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public string Filename { get; set; }
        public int Handle { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Texture() { }
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

            Width = img.Width;
            Height = img.Height;

            img.Mutate(x => x.Flip(FlipMode.Vertical));

            var pixels = new List<byte>();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var currentPixel = img[x, y];
                    pixels.Add(currentPixel.R);
                    pixels.Add(currentPixel.G);
                    pixels.Add(currentPixel.B);
                    pixels.Add(currentPixel.A);
                }
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
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
    }
}
