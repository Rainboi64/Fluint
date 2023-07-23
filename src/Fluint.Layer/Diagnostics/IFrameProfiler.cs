// 
// IFrameProfiler.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;

namespace Fluint.Layer.Diagnostics;

[Initialization(InitializationMethod.Scoped)]
public interface IFrameProfiler : IModule
{
    void Begin(string section);
    void End(string section);
    IDictionary<string, TimeSpan> GetDetails();
}