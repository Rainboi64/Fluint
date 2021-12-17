//
// Buffer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using OpenTK.Graphics.OpenGL4;
using System;

namespace Fluint.Engine.GL46.Graphics
{
    public class Buffer
    {
        public int Handle { get; set; }
        public BufferTarget BufferType { get; set; }

        public Buffer(BufferTarget BufferType)
        {
            this.BufferType = BufferType;
            Handle = GL.GenBuffer();
        }

        public void Bind() 
        {
            GL.BindBuffer(BufferType, Handle);
        }

        public void Unbind() 
        {
            GL.BindBuffer(BufferType, 0);
        }

        //Disposing Code

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                GL.DeleteProgram(Handle);

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Unbind();
            GL.DeleteBuffer(Handle);

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
