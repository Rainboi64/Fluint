//
// Texture.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = Fluint.Layer.Mathematics.Color;

namespace Fluint.Engine.GL46.Graphics
{
    public class Texture : ITexture
    {
        private string _filename;

        public Texture()
        {
        }

        public Texture(int width, int height, Color[] pixels)
        {
            Pixels = pixels;
            Size = new Vector2i(width, height);

            Handle = GL.GenTexture();

            Bind();
            SetupTextureFilters();
            Upload();
            Unbind();
        }

        public Texture(int width, int height)
        {
            Size = new Vector2i(width, height);

            Handle = GL.GenTexture();

            Bind();
            SetupTextureFilters();
            Unbind();
        }

        public Texture(string filename)
        {
            LoadFromFile(filename);
        }

        public int Handle
        {
            get;
            private set;
        }

        public Vector2i Size
        {
            get;
            private set;
        }

        public Color[] Pixels
        {
            get;
            private set;
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Handle);
        }

        public int ConvertIndex(int x, int y)
        {
            return x + Size.X * y;
        }

        public void Upload()
        {
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                Size.X,
                Size.Y,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                Pixels);
        }

        private static void SetupTextureFilters()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest);
        }

        public void LoadFromFile(string fileName)
        {
            _filename = fileName;

            Handle = GL.GenTexture();
            var img = (Image<Rgba32>)Image.Load(_filename);

            Bind();

            SetupTextureFilters();

            Size = new Vector2i(img.Width, img.Height);

            Pixels = new Color[img.Width * img.Height];

            img.Mutate(x => x.Flip(FlipMode.Vertical));

            for (var x = 0; x < img.Width; x++)
            {
                for (var y = 0; y < img.Height; y++)
                {
                    // I could use some bit-hack magicary in here.
                    // It would also be wise to use System.Drawing.
                    Pixels[ConvertIndex(x, y)] = new Color(
                        img[x, y].R,
                        img[x, y].G,
                        img[x, y].B);
                }
            }

            Upload();
        }

        public static implicit operator int(Texture textureView)
        {
            return textureView.Handle;
        }
    }
}