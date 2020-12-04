using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IParser : IModule
    {
        void Parse(string command, string[] args);
    }
}
