using System;
using System.Text;
using System.Collections.Generic;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;
using System.Linq;
using Fluint.Layer;

namespace Fluint.Implementation.SDK
{
    public class CommandLineListener : ICommandLineListener
    {
        private readonly IParser _parser;
        private List<ICommand> _commands;

        public CommandLineListener(ModulePacket packet)
        {
            _commands = packet.GetInstances().OfType<ICommand>().ToList();
            _parser = packet.CreateScoped<IParser>();
        }

        public void Execute(string command, string[] args)
        {
            _parser.Parse(command.ToLower(), args);
        }

        public void Listen()
        {
            while (true)
            {
                var builder = new StringBuilder();
                var newLine = false;

                var prompt = "> ";

                var commandDone = false;
                var commandLength = 0;

                while (!newLine)
                {
                    var left = Console.CursorLeft;
                    var top = Console.CursorTop;

                    Console.SetCursorPosition(prompt.Length + builder.Length, top);
                    var current = Console.ReadKey(false);
                    switch (current.Key)
                    {
                        case ConsoleKey.Enter:
                            newLine = true;
                            continue;

                        case ConsoleKey.Spacebar:
                            if (!commandDone)
                            {
                                commandLength = builder.Length;
                                commandDone = true;
                            }
                            break;

                        case ConsoleKey.Backspace:
                            if (builder.Length > 0)
                            {
                                if (builder.Length - commandLength <= 0)
                                {
                                    commandDone = false;
                                }
                                builder.Length--;
                            }
                            break;
                    }


                    if (!char.IsAscii(current.KeyChar) && !char.IsSymbol(current.KeyChar) && !char.IsLetterOrDigit(current.KeyChar) && !char.IsWhiteSpace(current.KeyChar)) continue;

                    builder.Append(current.KeyChar.ToString());
                    if (!commandDone)
                    {
                        var valid = GetLikelyCommands(builder.ToString()).Count > 0;
                        WritePrompt(0, top, "> ", valid);
                    }
                }

                var (command, arguments) = Parse(builder.ToString());
                Execute(command, arguments);
            }
        }

        private void RemoveLastChar()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        private void WritePrompt(int left, int top, string prompt, bool valid)
        {
            Console.SetCursorPosition(left, top);
            ConsoleHelper.Write(prompt, valid ? ConsoleColor.White : ConsoleColor.DarkRed);
        }

        private (string command, string[] arguments) Parse(string input)
        {
            var command = string.Empty;
            var arguments = new List<string>();

            var segments = input.Split(' ');

            command = segments.FirstOrDefault();

            return (command, arguments.ToArray());
        }

        private List<ICommand> GetLikelyCommands(string command)
        {
            return _commands.Where((x) => x.Command.StartsWith(command)).ToList();
        }

        private string GetCommandHelp(ICommand command)
        {
            return command
                .GetType()
                .GetCustomAttributes(false)?
                .OfType<ModuleAttribute>()
                .FirstOrDefault()?.Description;
        }

        private static IList<string> CutCommandRecomendations(List<ICommand> commands, string input)
        {
            return commands.Select((x) => x.Command.Remove(0, input.Length))
            .ToList();
        }

        public static void ClearLastLine(int lineLength)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', lineLength));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
