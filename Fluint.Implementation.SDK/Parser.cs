using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
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
        public ILogger _logger;
        public Parser(ModulePacket packet)
        {
            _commands = packet.GetInstances().OfType<ICommand>().ToList();
            _logger = packet.GetSingleton<ILogger>();
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
                _logger.Error("[Parser] command not found.");
        }

        private void List()
        {
            foreach (var consoleCommand in _commands)
            {
                var commandAttribute = consoleCommand.GetType().GetCustomAttributes(false).OfType<ModuleAttribute>().FirstOrDefault();
                Console.WriteLine($"[{consoleCommand.Command}] {commandAttribute.ModuleName}: {commandAttribute.Description}");
            }
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
                _logger.Error("[Parser] command not found.");
            }
        }

        public void Add(ICommand command)
        {
            _commands.Add(command);
        }
    }
}
