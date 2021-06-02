using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Networking
{
    [Initialization(InitializationMethod.Scoped)]
    public interface INetworkTaskManager
    {
        void Parse(string task);
    }
}
