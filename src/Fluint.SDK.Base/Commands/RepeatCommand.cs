// 
// RepeatCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

public class RepeatCommand : ICommand
{
    private readonly ModulePacket _packet;

    public RepeatCommand(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "repeat";

    public void Do(string[] args)
    {
        var length = args.Length;
        if (length < 2)
        {
            Console.WriteLine(
                "Invalid arguments, please supply the amount of times the command will be executed in the following format 'repeat {count} {command}'.");
            return;
        }

        if (!int.TryParse(args[0], out var count))
        {
            Console.WriteLine(
                "Invalid arguments, please supply the amount of times the command will be executed in the following format 'repeat {count} {command}'.");
            return;
        }

        var parser = _packet.CreateScoped<IParser>();

        for (var i = 0; i < count; i++)
        {
            parser.Parse(args[1], args.Skip(2).ToArray());
        }
    }
}