using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fluint.Layer.Diagnostics
{
    public static class LayerLogger
    {
        public static void InstantDump(object data)
        {
            var timeNow = DateTime.Now;
            File.WriteAllText($"./{timeNow}", data.ToString());
        }

        public static void LogToConsole(object data, bool dump = false)
        {
            if (!dump)
                Console.WriteLine("Layer Logger: {0}", data);
            else
            {
                var timeNow = DateTime.Now;
                File.WriteAllText($"./{timeNow}", data.ToString());
                Console.WriteLine("Layer Logger: {0}, dumped into {1}", data);
            }
        }

    }
}
