//
// TaskSchedule.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Tasks
{
    public enum TaskSchedule
    {
        Startup,

        // Window Related
        WindowReady,
        WindowUpdate,
        WindowRender,
        WindowDisposing,
        WindowResize,
        WindowEnterText,
        WindowMouseScroll,

        // Multi-threaded
        Background, 
    }
}
