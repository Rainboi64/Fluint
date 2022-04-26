//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API
{
    public interface IIndexBuffer : IDisposable
    {
        bool Is16Bit
        {
            get;
        }
    }
}