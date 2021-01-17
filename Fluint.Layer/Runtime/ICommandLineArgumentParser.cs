using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Runtime
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICommandLineArgumentParser : IModule
    {
        public void Parse(string command, string[] arguments);
    }
}
