//
// IShader.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.API
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IShader : IModule, IDisposable
    {
        void Enable();
        void Disable();
        void LoadPacket(ShaderPacket packet);
        void SetModelMatrix(Matrix matrix);
        void SetViewMatrix(Matrix matrix);
        void SetProjectionMatrix(Matrix matrix);
        void LoadSource(string vertexShaderSource, string pixelShaderSource);
    }
}