using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Runtime
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICommandLineArgumentParser
    {
        public void Parse(string command, string[] arguments);
    }
}
