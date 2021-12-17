//
// Parser.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluint.Implementation.SDK
{
    public class Parser : IParser
    {
        public IList<ICommand> _commands;
        public Parser(ModulePacket packet)
        {
            _commands = packet.GetInstances().OfType<ICommand>().ToList();
        }

        public void Parse(string command, string[] args)
        {
            var commands = _commands.Where(x => x.Command == command);

            if (commands.Any())
            {
                commands.FirstOrDefault().Do(args);
            }
            else if (command == "help")
                Help(args[0]);
            else if (command == "list")
                List();
            else
                ConsoleHelper.WriteEmbeddedColorLine($"'[blue]{command}[/blue]' couldn't be recognized as valid internal, or external command module.\nto see available commands try running '[blue]list[/blue]'");
        }

        private void List()
        {
            var table = new ConsoleTable();
            table.AddColumn(new[] {"Command", "Name", "Description" });
            foreach (var consoleCommand in _commands)
            {
                var commandAttribute = consoleCommand.GetType().GetCustomAttributes(false).OfType<ModuleAttribute>().FirstOrDefault();
                table.AddRow($"'{consoleCommand.Command}'", commandAttribute.ModuleName, commandAttribute.Description);
            }
            Console.WriteLine($"\n{table.ToMarkDownString()}");
        }

        private void Help(string command)
        {
            var specifiedCommand = _commands.Where(x => x.Command == command);
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

        public void Add(ICommand command)
        {
            _commands.Add(command);
        }
    }
}
