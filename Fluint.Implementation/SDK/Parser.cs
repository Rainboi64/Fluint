using Fluint.Layer;
using Fluint.Layer.Debugging;
using Fluint.Layer.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluint.Implementation.SDK
{
    public class Parser : IParser
    {
        public IEnumerable<ICommand> _commands;
        public ILogger _logger;
        public Parser(IEnumerable<IModule> instances, ILogger logger)
        {
            _commands = instances.OfType<ICommand>();
            _logger = logger;
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
    }
}
