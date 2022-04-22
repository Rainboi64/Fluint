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

namespace Fluint.Implementation.SDK
{
    public class Parser : IParser
    {
        public IList<ICommand> Commands;

        public Parser(ModulePacket packet)
        {
            Commands = packet.GetInstances().OfType<ICommand>().ToList();
        }

        public void Parse(string command, string[] args)
        {
            var commands = Commands.Where(x => x.Command == command);

            if (commands.Any())
            {
                commands.FirstOrDefault().Do(args);
            }
            else if (command == "help")
                Help(args[0]);
            else if (command == "list")
                List();
            else
                ConsoleHelper.WriteEmbeddedColorLine(
                    $"'[blue]{command}[/blue]' couldn't be recognized as valid internal, or external command module.\nto see available commands try running '[blue]list[/blue]'");
        }

        public void Add(ICommand command)
        {
            Commands.Add(command);
        }

        private void List()
        {
            var table = new ConsoleTable();
            table.AddColumn(new[] { "Command", "Name", "Description" });
            foreach (var consoleCommand in Commands)
            {
                var commandAttribute = consoleCommand.GetType().GetCustomAttributes(false).OfType<ModuleAttribute>()
                    .FirstOrDefault();
                table.AddRow($"'{consoleCommand.Command}'", commandAttribute.ModuleName, commandAttribute.Description);
            }

            Console.WriteLine($"\n{table.ToMarkDownString()}");
        }

        private void Help(string command)
        {
            var specifiedCommand = Commands.Where(x => x.Command == command);
            if (specifiedCommand.Any())
            {
                var commandAttribute = specifiedCommand.FirstOrDefault()
                    .GetType().GetCustomAttributes(false)
                    .OfType<ModuleAttribute>()
                    .FirstOrDefault();
                Console.WriteLine(commandAttribute.Help);
            }
            else
            {
            }
        }
    }
}