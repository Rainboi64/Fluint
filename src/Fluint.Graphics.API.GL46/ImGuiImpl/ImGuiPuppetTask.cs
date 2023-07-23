//
// ImGuiGhostCreationJob.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Jobs;
using Fluint.Layer.Windowing;

namespace Fluint.Graphics.API.GL46.ImGuiImpl;

public class ImGuiGhostCreationJob : IJob
{
    public JobSchedule Schedule => JobSchedule.WindowReady;
    public int Priority => 0;

    public void Start(JobArgs args)
    {
        var window = args.Invoker as IWindow;
        window.Puppet<ImGuiPuppet>();
    }
}