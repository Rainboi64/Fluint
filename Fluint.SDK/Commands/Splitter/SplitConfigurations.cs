using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.SDK.Commands.Splitter
{
    public sealed class SplitConfigurations
    {
        public string LayerLocation { get; set; }
        public IEnumerable<string> ProjectConfigurations { get; set; }
        public IEnumerable<Split> Splits { get; set; }
    }
}
