//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Graphics.API
{
    public interface IConstantBuffer : IModule, IDisposable
    {
        void UpdateBuffer<T>(T constants) where T : struct;
    }
}