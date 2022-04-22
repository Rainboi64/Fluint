//
//
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics
{
    public interface IConstantBuffer
    {
        void UpdateBuffer<T>(T constants) where T : struct;
    }
}