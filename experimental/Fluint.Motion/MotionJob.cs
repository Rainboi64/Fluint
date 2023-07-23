using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Jobs;
using Fluint.Layer.Windowing;

namespace Fluint.Motion;

public class MotionJob : IJob
{
    private readonly ModulePacket _packet;

    public MotionJob(ModulePacket packet)
    {
        _packet = packet;
    }

    public JobSchedule Schedule => JobSchedule.WindowReady;

    public int Priority => 1;

    public void Start(JobArgs args)
    {
        var window = args.Invoker as IWindow;
        window?.Enqueue(() => {
            // var mainMenu = window?.Controls["MainMenu"] as IMainMenu;
            // var menuItem = _packet.CreateScoped<IMenuItem>();
            // menuItem.Text = "Motion";
            // menuItem.OnClick = new ModularAction {
            //     () => window?.SpawnControl<MotionWindow>()
            // };
            // if (mainMenu != null)
            // {
            //     mainMenu["Motion"] = menuItem;
            // }
        });
    }
}