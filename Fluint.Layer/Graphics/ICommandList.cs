using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICommandList : ICollection<string>
    {
        void Flush();
    }
}
