using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    public interface ICommandList : ICollection<string>
    {
        void Flush();
    }
}
