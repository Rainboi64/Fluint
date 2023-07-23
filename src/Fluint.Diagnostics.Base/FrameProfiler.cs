// 
// FrameProfiler.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Fluint.Layer.Diagnostics;

namespace Fluint.Diagnostics.Base;

public class FrameProfiler : IFrameProfiler
{
    private readonly ConcurrentDictionary<string, TimeSpan> _details = new();
    private readonly Stopwatch _watch = new();

    public FrameProfiler()
    {
        _watch.Start();
    }

    public void Begin(string section)
    {
        _details[section] = _watch.Elapsed;
    }

    public void End(string section)
    {
        var start = _details[section];
        _details[section] = _watch.Elapsed - start;
    }

    public IDictionary<string, TimeSpan> GetDetails()
    {
        return _details;
    }
}