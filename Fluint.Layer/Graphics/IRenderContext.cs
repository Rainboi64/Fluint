using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IRendererContext
    {
        IntPtr Handle { get; }
        double Height { get; }
        double Width { get; }
    }
}
