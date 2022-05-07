//
// ILambdaParser.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.SDK
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ILambdaParser : IModule
    {
        void Add(ILambda command);
        LambdaObject Parse(string command, string[] args);
    }
}