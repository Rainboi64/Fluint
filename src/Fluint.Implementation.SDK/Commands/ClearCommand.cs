using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer;
using Fluint.Layer.SDK;

namespace Fluint.Implementation.SDK.Commands
{
    [Module("Clear Command", "Clears the sdk text", "enter the command to clean console window.")]
    public class ClearCommand : ICommand
    {
        public string Command => "clear";

        public void Do(string[] args)
        {
            Console.Clear();
        }
    }
}
