using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Runtime
{
    public interface ICommandLineArgumentParser
    {
        public void Parse(string command, string[] arguments);
    }
}
