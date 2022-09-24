// 
// Sleep.cs
// 
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Threading;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base;

public class Sleep : ILambda
{
    public string Command => "sleep";

    public LambdaObject Run(string[] args)
    {
        var length = args.Length;
        if (length != 1)
        {
            return LambdaObject.Error(
                "Invalid argument count, please supply the amount of milliseconds the thread will sleep for in the following format 'sleep {milliseconds}'.");
        }

        if (!int.TryParse(args[0], out var duration))
        {
            return LambdaObject.Error(
                "Invalid argument count, please supply the amount of milliseconds the thread will sleep for in the following format 'sleep {milliseconds}'.");
        }

        Thread.Sleep(duration);
        return LambdaObject.Success;
    }
}