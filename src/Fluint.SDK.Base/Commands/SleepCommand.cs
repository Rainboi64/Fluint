// 
// SleepCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Threading;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

public class SleepCommand : ICommand
{
    public string Command => "sleep";

    public void Do(string[] args)
    {
        var length = args.Length;
        if (length != 1)
        {
            Console.WriteLine(
                "Invalid argument count, please supply the amount of milliseconds the thread will sleep for in the following format 'sleep {milliseconds}'.");
            return;
        }

        if (!int.TryParse(args[0], out var duration))
        {
            Console.WriteLine(
                "Invalid argument count, please supply the amount of milliseconds the thread will sleep for in the following format 'sleep {milliseconds}'.");
            return;
        }

        Thread.Sleep(duration);
    }
}