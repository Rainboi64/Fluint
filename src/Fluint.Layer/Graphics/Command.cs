//
// Command.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

/**
 * @author Yaman Alhalabi <yamanalhalabi2@gmail.com>
 * @file A data type for enabling graphic api behavoir, needs to be passed to ICommandParser
 * @desc Created on 2020-12-11 7:50:25 pm
 * @copyright Panic Factory (C) 2020 
 */
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