//
// Benchmark.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Diagnostics;

namespace Fluint.Layer.Diagnostics;

public class Benchmark
{
    private readonly Action _action;

    private double _sampleTimes;
    public double AverageMillisecondDuration;
    public TimeSpan AverageTimeSpan;
    public TimeSpan BenchmarDuration;
    public DateTime BenchmarkEndTime;
    public long BenchmarkMillisecondsDuration;
    public DateTime BenchmarkStartTime;

    public uint TestSample;

    public Benchmark(Action action, uint testSample)
    {
        TestSample = testSample;
        _action = action;
    }

    public void Start()
    {
        var benchmarkWatch = new Stopwatch();

        BenchmarkStartTime = DateTime.Now;

        benchmarkWatch.Start();

        for (var i = 0; i < TestSample; i++)
        {
            var watch = new Stopwatch();

            watch.Start();
            _action.Invoke();
            watch.Stop();

            _sampleTimes += watch.ElapsedMilliseconds;
        }

        benchmarkWatch.Stop();

        BenchmarkMillisecondsDuration = benchmarkWatch.ElapsedMilliseconds;

        BenchmarkEndTime = DateTime.Now;

        BenchmarDuration = benchmarkWatch.Elapsed;
        AverageMillisecondDuration = _sampleTimes / TestSample;
        AverageTimeSpan = new TimeSpan(0, 0, 0, 0, (int)AverageMillisecondDuration);

        GC.Collect();
    }

    public override string ToString()
    {
        return
            $"Sample Size: {TestSample}\n" +
            $"Average Sample Time (ms): {AverageMillisecondDuration}\n" +
            $"Average Time: {AverageTimeSpan}\n" +
            $"Benchmark Time: {BenchmarDuration}\n" +
            $"Benchmark Start: {BenchmarkStartTime}\n" +
            $"Benchmark End: {BenchmarkEndTime}\n";
    }
}