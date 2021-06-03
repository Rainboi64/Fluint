//
// IShaderGenerator.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

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
