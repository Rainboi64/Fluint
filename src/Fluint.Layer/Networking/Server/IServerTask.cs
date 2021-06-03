//
// IServerTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Networking.Server 
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IServerTask : IModule
    {
        string TaskID { get; }
        void Do(ref IServerTaskHelper helper);
    }
}
