//
// ModuleAttribute.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer
{
    /// <summary>
    /// An attribute containing the name and the description of the module.
    /// </summary>
    public class ModuleAttribute : Attribute
    {
        public ModuleAttribute(string moduleName, string description)
        {
            ModuleName = moduleName;
            Description = description;
            Help = Description;
        }

        public ModuleAttribute(string moduleName, string description, string help)
        {
            ModuleName = moduleName;
            Description = description;
            Help = help;
        }


        /// <summary>
        /// The name of the module
        /// </summary>
        public string ModuleName { get; }

        /// <summary>
        /// The description of the module.
        /// </summary>
        public string Description { get; }

        public string Help { get; }
    }
}
