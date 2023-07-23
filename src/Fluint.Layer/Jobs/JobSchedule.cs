//
// JobSchedule.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Jobs;

public enum JobSchedule
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
    Background
}