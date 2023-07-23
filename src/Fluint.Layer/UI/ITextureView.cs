using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface ITextureView : IModule, IGuiComponent
{
    Vector2i Size
    {
        get;
        set;
    }

    ITexture Texture
    {
        get;
        set;
    }
}