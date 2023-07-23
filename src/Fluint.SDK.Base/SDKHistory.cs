// 
// SDKHistory.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.Configuration;

namespace Fluint.SDK.Base;

public class SDKHistory : IConfiguration
{
    public List<string> CommandHistory
    {
        get;
        set;
    } = new();
}