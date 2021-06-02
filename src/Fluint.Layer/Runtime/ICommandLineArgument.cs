using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Runtime
{
    [Initialization(InitializationMethod.Instanced)]
    public interface ICommandLineArgument : IModule
    {
        string CommandText { get; set; }
        void Execute();
    }
}
