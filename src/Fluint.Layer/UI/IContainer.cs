using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IContainer : IModule, IGuiComponent
    {
        string Title { get; set; }
    }
}
