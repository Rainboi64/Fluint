using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Layer.UI
{
    public class Font
    {
        public Action EnableFont { get; }
        public Action DisableFont { get; }

        public Font(Action enable, Action disable)
        {
            EnableFont = enable;
            DisableFont = disable;
        }
    }
}
