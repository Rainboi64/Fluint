using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICanvasRenderer : IModule, IDisposable
    {
        public ICanvas Canvas { get; set; }
        public void Create();
        public void Render();
        public void Destroy();
    }
}
