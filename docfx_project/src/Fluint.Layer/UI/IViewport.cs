using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IViewport : IModule, IGuiComponent
    {
        public Vector2i Size { get; set; }
    }
}