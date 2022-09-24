//
// Exit.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Lambdas.Base
{
    [Module("Exit Command", "Exits the application with code 0", "enter this command to exit the application")]
    public class Exit : ILambda
    {
        public string Command => "exit";

        public LambdaObject Run(string[] args)
        {
            Environment.Exit(0);

            return LambdaObject.Success;
        }
    }
}