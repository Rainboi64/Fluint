using Fluint.Layer.Configuration;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Windowing;

[Configuration("Window Configuration", "A Configuration for the display a Fluint Window.", "Windowing")]
public class WindowConfiguration : IConfiguration
{
    public WindowConfiguration()
    {
        VSync = true;
        Resizable = true;
        WindowSize = new Vector2i(1600, 900);
        FrameLimit = 60;
    }

    public bool VSync
    {
        get;
        set;
    }

    public Vector2i WindowSize
    {
        get;
        set;
    }

    public bool Resizable
    {
        get;
        set;
    }

    public int FrameLimit
    {
        get;
        set;
    }
}