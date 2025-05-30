// 
// IDebugRepository.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.Diagnostics;

[Initialization(InitializationMethod.Singleton)]
public interface IDebugRepository : IModule
{
    public IDictionary<string, string> Items
    {
        get;
    }
}