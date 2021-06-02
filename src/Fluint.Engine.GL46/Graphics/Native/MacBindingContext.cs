using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Engine.GL46.Graphics.Native
{
#if Target_Mac
    public class MacBindingContext : IBindingsContext
    {

        public IntPtr GetProcAddress(string procName)
        {
            throw new NotImplementedException();
        }
    }
#endif
}
