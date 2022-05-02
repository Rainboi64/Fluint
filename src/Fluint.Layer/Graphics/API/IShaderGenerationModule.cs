//
// IShaderGenerationModule.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics.API
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IShaderGenerationModule
    {
        ShaderStage Stage
        {
            get;
        }

        /// <summary>
        /// Sets the priority of this module, the lower numbers will be concatenated first.
        /// </summary>
        int Priority
        {
            get;
        }

        /// <summary>
        /// Generates the code to concatenate.
        /// </summary>
        /// <returns>Returns the generated code.</returns>
        string Generate();
    }
}