//
// IShader.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//


namespace Fluint.Layer.Graphics
{
    public interface IShader : IModule
    {
        void Enable();
        void Disable();
        void SetModelMatrix(Mathematics.Matrix matrix);
        void LoadSource(string vertexShaderSource, string pixelShaderSource);
    }
}