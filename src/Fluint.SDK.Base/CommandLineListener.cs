using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base
{
    public class CommandLineListener : ICommandLineListener
    {
        private readonly List<ICommand> _commands;
        private readonly List<string> _history = new();
        private readonly IParser _parser;

        private Dictionary<string, Action> _keyActions = new Dictionary<string, Action>();

        public CommandLineListener(ModulePacket packet)
        {
            _commands = packet.GetInstances().OfType<ICommand>().ToList();
            _parser = packet.CreateScoped<IParser>();
        }

        public void Listen()
        {
            while (true)
            {
                var command = ReadLine();
                Call(command);
            }
        }

        public void Execute(string command, string[] args)
        {
            _parser.Parse(command.ToLower(), args);
        }

        private string ReadLine()
        {
            var keyHandler = new ConsoleKeyHandler(_history);

            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                    break;

                keyHandler.Handle(keyInfo);
            }

            var text = keyHandler.GetText();

            if (string.IsNullOrWhiteSpace(text)) text = default;

            Console.WriteLine();

            _history.Add(text);
            return text;
        }

        private void Call(string input)
        {
            AddToHistory(input);

            var (command, arguments) = Parse(input);
            Execute(command, arguments);
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        private (string command, string[] arguments) Parse(string input)
        {
            bool inQuotes = false;

            var segments = Split(input, c => {
                    if (c == '\"')
                    {
                        inQuotes = !inQuotes;
                    }

                    return !inQuotes && c == ' ';
                })
                .Select(arg => TrimMatchingQuotes(arg.Trim(), '\"'))
                .Where(arg => !string.IsNullOrEmpty(arg));

            var command = segments.FirstOrDefault();
            var arguments = segments.Skip(1);

            return (command, arguments.ToArray());
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        public static IEnumerable<string> Split(string str, Func<char, bool> controller)
        {
            var nextPiece = 0;

            for (int c = 0; c < str.Length; c++)
            {
                if (controller(str[c]))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }
            }

            yield return str.Substring(nextPiece);
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        public static string TrimMatchingQuotes(string input, char quote)
        {
            if ((input.Length >= 2) &&
                (input[0] == quote) && (input[input.Length - 1] == quote))
                return input.Substring(1, input.Length - 2);

            return input;
        }

        private List<ICommand> GetLikelyCommands(string command)
        {
            return _commands.Where((x) => x.Command.StartsWith(command)).ToList();
        }

        private void AddToHistory(string input)
        {
            _history.Add(input);
        }

        private ModuleAttribute GetCommandAttributes(ICommand command) => command
            .GetType()
            .GetCustomAttributes(false)?
            .OfType<ModuleAttribute>()
            .FirstOrDefault();
    }
}