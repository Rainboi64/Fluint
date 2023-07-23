// 
// Shader.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Miscellaneous;

namespace Fluint.Layer.Graphics.API;

abstract public class Shader : IShader
{
    protected Shader()
    {
        Macros = new Dictionary<string, string>();
    }

    protected Dictionary<string, string> Macros
    {
        get;
    }

    public IInputLayout InputLayout
    {
        get;
        protected set;
    }

    public void CompileFile(ShaderStage shaderStage, string filePath, VertexType vertexType)
    {
        CompileFileInternal(shaderStage, filePath, vertexType);
    }

    public void CompileString(ShaderStage shaderStage, string shaderText, VertexType vertexType)
    {
        CompileStringInternal(shaderStage, shaderText, vertexType);
    }

    abstract public void Dispose();

    public void AddMacro(string name, string value)
    {
        if (!Macros.TryAdd(name, value))
        {
            $"Key already exists {name}.".Print();
        }
    }

    abstract protected void CompileFileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType);

    abstract protected void CompileStringInternal(ShaderStage shaderStage, string filePath, VertexType vertexType);
}