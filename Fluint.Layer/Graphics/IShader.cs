//
// IShader.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//


namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IShader : IModule
    {
        void Enable();
        void Disable();
        void LoadPacket(ShaderPacket packet);
        void SetModelMatrix(Mathematics.Matrix matrix);
        void SetViewMatrix(Mathematics.Matrix matrix);
        void SetProjectionMatrix(Mathematics.Matrix matrix);
        void LoadSource(string vertexShaderSource, string pixelShaderSource);
    }
}