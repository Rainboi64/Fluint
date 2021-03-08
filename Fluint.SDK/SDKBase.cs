using Fluint.Layer.Debugging;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;
using Fluint.SDK.Commands;
using Fluint.SDK.Commands.Splitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluint.SDK
{
    public class SDKBase
    {
        private readonly IParser _parser;
        public SDKBase(ModulePacket packet) 
        {
            _parser = packet.GetScoped<IParser>();

            _parser.Add((ICommand)packet.CreateInstance(typeof(AboutCommand)));
            _parser.Add((ICommand)packet.CreateInstance(typeof(LanguageCommand)));
            _parser.Add((ICommand)packet.CreateInstance(typeof(SplitterCommand)));
        }

        public void Execute(string command, string[] args)
        {
            _parser.Parse(command.ToLower(), args);
        }

        public void Listen() 
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();

                var input = Console.ReadLine().Split(' ');

                var command = input[0];
                Execute(command, input.Skip(1).ToArray());
            }
        }
    }
}
