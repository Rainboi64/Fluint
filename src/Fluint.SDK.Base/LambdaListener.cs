using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.SDK;
using Newtonsoft.Json;

namespace Fluint.SDK.Base
{
    public class LambdaListener : ILambdaListener
    {
        private readonly IConfigurationManager _configurationManager;
        private readonly ILambdaParser _lambdaParser;
        private readonly PromptConfiguration _promptConfiguration;
        private readonly SDKHistory _sdkHistory;

        private string _prompt;

        public LambdaListener(ModulePacket packet)
        {
            var lambdas = packet.GetInstances().OfType<ILambda>().ToList();
            _lambdaParser = packet.CreateScoped<ILambdaParser>();

            _configurationManager = packet.GetSingleton<IConfigurationManager>();
            _sdkHistory = _configurationManager.Get<SDKHistory>();

            _promptConfiguration = _configurationManager.Get<PromptConfiguration>();
            _prompt = _promptConfiguration.DefaultPrompt;
        }

        public void Execute(string command)
        {
            if (command.Length <= 0)
            {
                return;
            }

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
            var keyHandler = new ConsoleKeyHandler(_sdkHistory.CommandHistory);

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

            if (string.IsNullOrWhiteSpace(text))
            {
                text = "";
            }
            else
            {
                AddToHistory(text);
            }

            return text;
        }

        private void Call(string input)
        {
            if (input is null || input.Length <= 0)
            {
                Console.WriteLine();
                return;
            }

            var timer = new Stopwatch();
            timer.Start();

            var (command, arguments) = Parse(input);
            var response = Execute(command, arguments);

            timer.Stop();

            var lambdaColor = response.Status switch {
                LambdaStatus.Success => "green",
                LambdaStatus.Failure => "red",
                _ => "yellow"
            };

            _prompt = string.Format(_promptConfiguration.Prompt, lambdaColor, timer.ElapsedMilliseconds / 1000f);

            var json = JsonConvert.SerializeObject(response.Data);
            var prettifiedJson = ColorizeJson(json);

            // Pad with an extra newline.
            Console.WriteLine();
            ConsoleHelper.WriteEmbeddedColorLine(prettifiedJson);
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        private static IEnumerable<string> Split(string str, Func<char, bool> controller)
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

            yield return str[nextPiece..];
        }

        // From https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        private static string TrimMatchingQuotes(string input, char quote)
        {
            if ((input.Length >= 2) &&
                input[0] == quote && input[^1] == quote)
            {
                return input.Substring(1, input.Length - 2);
            }

            return input;
        }

        private void AddToHistory(string input)
        {
            _sdkHistory.CommandHistory.Add(input);
            _configurationManager.Add(_sdkHistory);
        }

        private static string ColorizeJson(string json)
        {
            var stringBuilder = new StringBuilder(json);

            stringBuilder.Replace(@"\n", "\n");

            // TODO: Add syntax highlighting for json.

            return stringBuilder.ToString();
        }
    }
}