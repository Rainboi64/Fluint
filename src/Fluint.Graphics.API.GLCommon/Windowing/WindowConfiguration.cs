using Fluint.Layer.Configuration;

namespace Fluint.Engine.GLCommon.Windowing;

[Configuration("Window Configuration", "A Configuration for the OpenGL46 window provider.", "OpenGL46")]
public class WindowConfiguration : IConfiguration
{
    public WindowConfiguration()
    {
        VSync = true;
    }

    public bool VSync
    {
        get;
        set;
    }
}