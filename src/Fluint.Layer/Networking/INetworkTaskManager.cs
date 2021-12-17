//
// INetworkTaskManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

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
