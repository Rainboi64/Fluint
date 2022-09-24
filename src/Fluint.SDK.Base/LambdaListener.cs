using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.SDK;
using Newtonsoft.Json;

namespace Fluint.SDK.Base
{
    public class LambdaListener : ILambdaListener
    {
        private readonly List<ILambda> _commands;
        private readonly List<string> _history = new();
        private readonly ILambdaParser _lambdaParser;

        private string _prompt = "[magenta]λ[/magenta] ";

        public LambdaListener(ModulePacket packet)
        {
            _commands = packet.GetInstances().OfType<ILambda>().ToList();
            _lambdaParser = packet.CreateScoped<ILambdaParser>();
        }

        public void Execute(string command)
        {
            var (onlyCommand, arguments) = Parse(command);
            Execute(onlyCommand, arguments);
        }

        public void Listen()
        {
            while (true)
            {
                var command = ReadLine();
                Call(command);
            }
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        public (string command, string[] arguments) Parse(string input)
        {
            var inQuotes = false;

            var segments = Split(input, c => {
                    if (c == '\"')
                    {
                        inQuotes = !inQuotes;
                    }

                    return !inQuotes && c == ' ';
                })
                .Select(arg => TrimMatchingQuotes(arg.Trim(), '\"'))
                .Where(arg => !string.IsNullOrEmpty(arg));

            var segmentArray = segments as string[] ?? segments.ToArray();
            var command = segmentArray.FirstOrDefault();
            var arguments = segmentArray.Skip(1);

            return (command, arguments.ToArray());
        }

        private LambdaObject Execute(string command, string[] args)
        {
            return _lambdaParser.Parse(command.ToLower(), args);
        }

        private string ReadLine()
        {
            var keyHandler = new ConsoleKeyHandler(_history);

            ConsoleHelper.WriteEmbeddedColor(_prompt);

            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

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
            var timer = new Stopwatch();
            timer.Start();

            AddToHistory(input);

            var (command, arguments) = Parse(input);
            var response = Execute(command, arguments);

            timer.Stop();

            var lambdaColor = response.Status switch {
                LambdaStatus.Success => "green",
                LambdaStatus.Failure => "red",
                _ => "yellow"
            };

            _prompt = $"[{lambdaColor}]λ[/{lambdaColor}] [took [yellow]{timer.ElapsedMilliseconds / 1000f}s[/yellow]] ";
            Console.WriteLine(JsonConvert.SerializeObject(response.Data));
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        public static IEnumerable<string> Split(string str, Func<char, bool> controller)
        {
            var nextPiece = 0;

            for (var c = 0; c < str.Length; c++)
            {
                if (!controller(str[c]))
                {
                    continue;
                }

                yield return str.Substring(nextPiece, c - nextPiece);
                nextPiece = c + 1;
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

        private List<ILambda> GetLikelyCommands(string command)
        {
            return _commands.Where((x) => x.Command.StartsWith(command)).ToList();
        }

        private void AddToHistory(string input)
        {
            _history.Add(input);
        }

        private ModuleAttribute GetCommandAttributes(ILambda command)
        {
            return command
                .GetType()
                .GetCustomAttributes(false)?
                .OfType<ModuleAttribute>()
                .FirstOrDefault();
        }
    }
}