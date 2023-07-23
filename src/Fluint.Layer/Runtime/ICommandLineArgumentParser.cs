//
// ICommandLineArgumentParser.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Runtime;

[Initialization(InitializationMethod.Scoped)]
public interface ICommandLineArgumentParser : IModule
{
    public void Parse(string command, string[] arguments);
}