//
// Clear.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base
{
    [Module("Clear Command", "Clears the sdk text", "enter the command to clean console window.")]
    public class Clear : ILambda
    {
        public string Command => "clear";

        public LambdaObject Run(string[] args)
        {
            Console.Clear();
            return LambdaObject.Success;
        }
    }
}