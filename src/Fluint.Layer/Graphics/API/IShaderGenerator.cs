//
// IShaderGenerator.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics.API;

[Initialization(InitializationMethod.Scoped)]
public interface IShaderGenerator
{
    public IShader Generate();
    public void Add(IShaderGenerationModule module);
}