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
