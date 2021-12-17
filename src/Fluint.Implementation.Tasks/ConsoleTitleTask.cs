//
// ConsoleTitleTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Diagnostics;
using System.Threading;
using Fluint.Layer.Tasks;

namespace Fluint.Implementation.Tasks
{
    public class ConsoleTitleTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.Background;
        public int Priority => 1;

        public void Start(TaskArgs args)
        {
            var proc = Process.GetCurrentProcess();
            while (true)
            {
                Console.Title = $"Fluint MEM: {proc.WorkingSet64 / (1024 * 1024)}MBs";

                Thread.Sleep(1000);
            }
        }
    }
}
