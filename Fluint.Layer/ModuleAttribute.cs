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
        }

        /// <summary>
        /// The name of the module
        /// </summary>
        public string ModuleName { get; }

        /// <summary>
        /// The description of the module.
        /// </summary>
        public string Description { get; }
    }
}
