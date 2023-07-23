using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Jobs;
using Fluint.Layer.Windowing;

namespace Fluint.Graphics.RayTracer;

public class DemoJob : IJob
{
    private readonly ModulePacket _packet;

    public DemoJob(ModulePacket packet)
    {
        _packet = packet;
    }

    public JobSchedule Schedule => JobSchedule.WindowReady;

    public int Priority => 1;

    public void Start(JobArgs args)
    {
        var window = args.Invoker as IWindow;
        window?.Enqueue(() => {
            //     var mainMenu = window?.Controls["MainMenu"] as IMainMenu;
            //     var menuItem = _packet.CreateScoped<IMenuItem>();
            //     menuItem.Text = "Ray Tracer";
            //     menuItem.OnClick = new ModularAction {
            //         () => window?.SpawnControl<DemoWindow>()
            //     };
            //     if (mainMenu != null)
            //     {
            //         mainMenu["Ray Tracer"] = menuItem;
            //     }
        });
    }
}