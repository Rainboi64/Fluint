//
// Buffer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46.Graphics
{
    public class Buffer
    {
        //Disposing Code

        private bool _disposedValue;

        public Buffer(BufferTarget bufferType)
        {
            BufferType = bufferType;
            Handle = GL.GenBuffer();
        }

        public int Handle
        {
            get;
            set;
        }

        public BufferTarget BufferType
        {
            get;
            set;
        }

        public void Bind()
        {
            GL.BindBuffer(BufferType, Handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferType, 0);
        }

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