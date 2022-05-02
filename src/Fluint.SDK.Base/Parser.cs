//
// Parser.cs
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
    public class Parser : IParser
    {
        private readonly IList<ICommand> _commands;

        public Parser(ModulePacket packet)
        {
            _commands = packet.GetInstances()
                .OfType<ICommand>()
                .ToList();
        }

        public void Parse(string command, string[] args)
        {
            var commandObject = GetCommandByString(command);

            if (commandObject is not null)
            {
                commandObject.Do(args);
            }
            else
            {
                switch (command)
                {
                    case "help":
                        Help(GetCommandByString(args.FirstOrDefault()));
                        break;
                    case "list":
                        List();
                        break;
                    default:
                        ConsoleHelper.WriteEmbeddedColorLine(
                            $"'[blue]{command}[/blue]' is not recognized as a [red]Fluint[/red] command module.\n" +
                            "to see available commands try running '[blue]list[/blue]'");
                        break;
                }
            }
        }

        public void Add(ICommand command)
        {
            _commands.Add(command);
        }

        private ICommand GetCommandByString(string command)
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

        private static void Help(ICommand command)
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