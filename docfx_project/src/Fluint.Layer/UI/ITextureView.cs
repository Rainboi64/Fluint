using Fluint.Layer.Graphics;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITextureView : IModule, IGuiComponent
    {
        ITexture Texture { get; set; }
    }
}