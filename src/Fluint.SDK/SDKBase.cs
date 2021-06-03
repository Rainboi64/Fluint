//
// SDKBase.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Diagnostics;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.SDK;
using Fluint.SDK.Commands;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();

                var input = Console.ReadLine().Split(' ');

                var command = input[0];
                Execute(command, input.Skip(1).ToArray());
            }
        }
    }
}
