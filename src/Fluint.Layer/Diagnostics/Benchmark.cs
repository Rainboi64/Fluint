//
// Benchmark.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Fluint.Layer.Diagnostics
{
    public class Benchmark
    {
        public DateTime BenchmarkStartTime;
        public DateTime BenchmarkEndTime;
        public TimeSpan BenchmarDuration;
        public TimeSpan AverageTimeSpan;
        public double AverageMillisecondDuration;
        public long BenchmarkMillisecondsDuration;

        private double _sampleTimes = 0;

        public uint TestSample;

        private readonly Action _action;

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

            for (int i = 0; i < TestSample; i++)
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
            AverageMillisecondDuration = (double)_sampleTimes / (double)TestSample;
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
}
