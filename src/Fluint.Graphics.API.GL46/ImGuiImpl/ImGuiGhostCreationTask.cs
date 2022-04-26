//
// ImGuiGhostCreationTask.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Tasks;
using Fluint.Layer.Windowing;

namespace Fluint.Engine.GL46.ImGuiImpl
{
    public class ImGuiGhostCreationTask : ITask
    {
        public TaskSchedule Schedule => TaskSchedule.WindowReady;
        public int Priority => 0;

        public void Start(TaskArgs args)
        {
            var window = args.Invoker as IWindow;
            window.AdoptGhost<ImGuiGhost>();
        }
    }
}