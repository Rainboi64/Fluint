using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Runtime
{
    public interface ICommandLineArgument
    {
        string CommandText { get; set; }
        void Execute();
    }
}
