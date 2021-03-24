// -------------------------------------------------------------------------
// IShaderGenerator.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//
// Description:
// This is the abstraction for the Shader Generator which is an object,
// that uses IShaderGenerationModule for generating custom shader code.
//
// References:
// 1. https://ep.liu.se/ecp/013/005/ecp01305.pdf
//
// -------------------------------------------------------------------------

using System.Collections.Generic;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IShaderGenerator
    {
        public IShader Generate();
        public void Add(IShaderGenerationModule module);
    }
}
