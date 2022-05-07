//
// LambdaParser.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base
{
    public class LambdaParser : ILambdaParser
    {
        private readonly IList<ILambda> _commands;

        public LambdaParser(ModulePacket packet)
        {
            _commands = packet.GetInstances()
                .OfType<ILambda>()
                .ToList();
        }

        public LambdaObject Parse(string command, string[] args)
        {
            if (command.StartsWith("#"))
            {
                return LambdaObject.Unknown;
            }

            var commandObject = GetCommandByString(command);

            if (commandObject is not null)
            {
                return commandObject.Run(args);
            }

            switch (command)
            {
                case "help":
                    Help(GetCommandByString(args.FirstOrDefault()));
                    break;
                case "list":
                    List();
                    break;
                default:
                    return LambdaObject.Error("Lambda not found");
            }

            return LambdaObject.Unknown;
        }

        public void Add(ILambda command)
        {
            _commands.Add(command);
        }

        private ILambda GetCommandByString(string command)
        {
            foreach (var item in _commands)
            {
                if (item.Command == command.ToLower())
                {
                    return item;
                }
            }

            return null;
        }

        private void List()
        {
            var table = new ConsoleTable();

            table.AddColumn(new[] { "Command", "Name", "Description" });
            foreach (var consoleCommand in _commands)
            {
                var commandAttribute = consoleCommand.GetType()
                    .GetCustomAttributes(false)
                    .OfType<ModuleAttribute>()
                    .FirstOrDefault();

                table.AddRow($"'{consoleCommand.Command}'", commandAttribute?.ModuleName,
                    commandAttribute?.Description);
            }

            Console.WriteLine($"\n{table.ToMarkDownString()}");
        }

        private static void Help(ILambda command)
        {
            if (command is null)
            {
                Console.WriteLine("The help command requires a second argument as command.");
                return;
            }

            var attribute = command
                .GetType()
                .GetCustomAttributes(typeof(ModuleAttribute), false)
                .FirstOrDefault() as ModuleAttribute;

            if (attribute is null)
            {
                Console.WriteLine("This command does not offer help");
                return;
            }

            if (attribute.Help is null)
            {
                Console.WriteLine("This command does not offer help");
                return;
            }

            Console.WriteLine(attribute.Help);
        }
    }
}