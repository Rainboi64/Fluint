//
// ILambda.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.SDK;

[Initialization(InitializationMethod.Instanced)]
public interface ILambda : IModule
{
    string Command
    {
        get;
    }

    LambdaObject Run(string[] args);
}