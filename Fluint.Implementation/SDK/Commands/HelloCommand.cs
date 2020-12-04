using Fluint.Layer.SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Implementation.SDK.Commands
{
    public class HelloCommand : ICommand
    {
        public string Command => "hello";

        public void Do(string[] args)
        {
            Console.WriteLine("peeps be like lmfao");
        }
    }
}
