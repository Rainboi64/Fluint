using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.DependencyInjection
{
    public sealed class ModuleManifest 
    {
        public bool Active { get; set; }
        public string OriginalAssembly { get; set; }
        public string SplitAssembly { get; set; }
    }
}
