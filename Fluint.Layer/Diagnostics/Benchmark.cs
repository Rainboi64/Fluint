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
        public long AverageMillisecondDuration;
        public long BenchmarkMillisecondsDuration;

        public List<long> SampleTimes = new List<long>();

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

                SampleTimes.Add(watch.ElapsedMilliseconds);
            }

            benchmarkWatch.Stop();

            BenchmarkMillisecondsDuration = benchmarkWatch.ElapsedMilliseconds;

            BenchmarkEndTime = DateTime.Now;

            for (int i = 0; i < TestSample; i++)
            {
                var sample = SampleTimes[i];
                AverageMillisecondDuration += sample;
            }

            AverageMillisecondDuration /= TestSample;
            AverageTimeSpan = new TimeSpan(0, 0, 0, 0, (int)AverageMillisecondDuration);
        }
    }
}
