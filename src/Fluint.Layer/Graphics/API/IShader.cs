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
        IInputLayout InputLayout { get; }
        void CompileFile(ShaderStage shaderStage, string filePath, VertexType vertexType);
        void CompileString(ShaderStage shaderStage, string shaderText, VertexType vertexType);
    }
}