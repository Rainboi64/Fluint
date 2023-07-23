//
// ICommandLineArgument.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Runtime;

[Initialization(InitializationMethod.Instanced)]
public interface ICommandLineArgument : IModule
{
    string CommandText
    {
        get;
        set;
    }

    void Execute();
}