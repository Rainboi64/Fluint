//
// GL46Texture.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46
{
    public class GL46Texture : ITexture
    {
        private readonly int _height;
        private readonly int _nativeTexture;
        private readonly int _width;

        public GL46Texture(int width, int height, Filter filter, TextureAddressMode textureAddressMode,
            ref byte[] pixelData)
        {
            _width = width;
            _height = height;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out _nativeTexture);
            GL.TextureStorage2D(_nativeTexture, 1, SizedInternalFormat.Rgba32f, width, height);

            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapT, (int)textureAddressMode.ToOpenTK());

            GL.TextureSubImage2D(_nativeTexture, 0, 0, 0, width, height, PixelFormat.Rgba, PixelType.UnsignedByte,
                pixelData);

            View = new GL46TextureView(this);
        }

        public GL46Texture(int width, int height, Filter filter, TextureAddressMode textureAddressMode)
        {
            _width = width;
            _height = height;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out _nativeTexture);
            GL.TextureStorage2D(_nativeTexture, 1, SizedInternalFormat.Rgba32f, width, height);

            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapT, (int)textureAddressMode.ToOpenTK());


            View = new GL46TextureView(this);
        }

        public GL46Texture(int width, int height, Filter filter)
        {
            _width = width;
            _height = height;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out _nativeTexture);
            GL.BindTexture(TextureTarget.Texture2D, _nativeTexture);

            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
            GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());

            View = new GL46TextureView(this);
        }

        public int Handle => _nativeTexture;

        public TextureView View
        {
            get;
        }

        public void Dispose()
        {
            GL.DeleteTexture(_nativeTexture);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                _width,
                _height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                data);
        }

        public static implicit operator int(GL46Texture texture)
        {
            return texture._nativeTexture;
        }
    }
}