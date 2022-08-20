// 
// Echo.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Lambdas;

public class Echo : ILambda
{
    public string Command => "echo";

    public LambdaObject Run(string[] args)
    {
        foreach (var item in args)
        {
            Console.WriteLine(item);
        }

        return LambdaObject.Success;
        ;
    }
}