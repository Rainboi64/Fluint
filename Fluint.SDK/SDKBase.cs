using Fluint.Layer.Debugging;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;
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
                Console.Write("Fluint.SDK>");
                Console.ResetColor();
                var input = Console.ReadLine().Split(' ');
                var command = input[0];
                Execute(command, input.Skip(1).ToArray());
            }
        }
    }
}
