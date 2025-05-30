//
// Command.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics.API;

public struct Command
{
    public const string LoadContext = "LOAD_CONTEXT";

    public Command(string commandIdentifier, params object[] arguments)
    {
        CommandIdentifier = commandIdentifier;
        Arguments = arguments;
    }

    public readonly string CommandIdentifier;
    public readonly object[] Arguments;
}