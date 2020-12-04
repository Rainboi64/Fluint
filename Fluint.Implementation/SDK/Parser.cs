using Fluint.Layer;
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
        public Parser(IEnumerable<IModule> instances)
        {
            _commands = instances.OfType<ICommand>();
        }

        public void Parse(string command, string[] args)
        {
            _commands.Where(x => x.Command == command).FirstOrDefault().Do(args);
        }
    }
}
