//
// IShaderGenerationModule.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IShaderGenerationModule
    {
        ShaderType Type { get; }

        /// <summary>
        /// Sets the priority of this module, the lower numbers will be concatenated first.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Generates the code to concatenate.
        /// </summary>
        /// <returns>Returns the generated code.</returns>
        string Generate();
    }
}
