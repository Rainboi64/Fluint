using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.SDK.Commands.Splitter
{
    public sealed class Split
    {
        public string Name { get; set; }
        public IEnumerable<string> ProjectConfigurations { get; set; }
        public IEnumerable<string> Dependencies { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
