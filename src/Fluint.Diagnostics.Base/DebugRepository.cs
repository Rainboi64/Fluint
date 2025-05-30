// 
// DebugRepository.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Diagnostics;

namespace Fluint.Diagnostics.Base;

public class DebugRepository : IDebugRepository
{
    public IDictionary<string, string> Items
    {
        get;
    } = new Dictionary<string, string>();
}