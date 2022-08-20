// 
// Shader.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using System.Diagnostics;

namespace Fluint.Layer.Graphics.API;


abstract public class Shader : IShader
{
    public IInputLayout InputLayout { get; protected set; }
    protected Dictionary<string, string> Macros { get; }

    public void AddMacro(string name, string value)
    {
        if (!Macros.TryAdd(name, value))
        {
            Debug.WriteLine($"Key already exists {name}.");
        }
    }

    protected abstract void CompileFileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType);

    protected abstract void CompileStringInternal(ShaderStage shaderStage, string filePath, VertexType vertexType);

    public void CompileFile(ShaderStage shaderStage, string filePath, VertexType vertexType)
    {
        CompileFileInternal(shaderStage, filePath, vertexType);
    }

    public void CompileString(ShaderStage shaderStage, string shaderText, VertexType vertexType)
    {
        CompileStringInternal(shaderStage, shaderText, vertexType);
    }

    public abstract void Dispose();

    protected Shader()
    {
        Macros = new Dictionary<string, string>();
    }
}