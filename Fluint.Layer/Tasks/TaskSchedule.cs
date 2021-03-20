using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Tasks
{
    public enum TaskSchedule
    {
        Startup,
        
        // Renderer Related
        RendererReady,
        PreRender,
        PostRender,
        RendererDisposing,

        Background,
    }
}
