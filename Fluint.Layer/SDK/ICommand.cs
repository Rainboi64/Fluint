using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Instanced)]
    public interface ICommand : IModule
    {
        string Command { get; }
        void Do(string[] args);
    }
}
