using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IViewport : IModule, IGuiComponent
    {
        public ISwapChain SwapChain
        {
            get;
            set;
        }
        public Vector2i Size { get; set; }
    }
}