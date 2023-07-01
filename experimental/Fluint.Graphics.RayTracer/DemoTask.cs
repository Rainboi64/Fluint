using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;
using Fluint.Layer.Tasks;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Graphics.RayTracer;

public class DemoTask : ITask
{
    private readonly ModulePacket _packet;

    public DemoTask(ModulePacket packet)
    {
        _packet = packet;
    }

    public TaskSchedule Schedule => TaskSchedule.WindowReady;

    public int Priority => 1;

    public void Start(TaskArgs args)
    {
        var window = args.Invoker as IWindow;
        window?.Enqueue(() => {
            var mainMenu = window?.Controls["MainMenu"] as IMainMenu;
            var menuItem = _packet.CreateScoped<IMenuItem>();
            menuItem.Text = "Ray Tracer";
            menuItem.OnClick = new ModularAction {
                () => window?.SpawnControl<DemoWindow>()
            };
            if (mainMenu != null)
            {
                mainMenu["Ray Tracer"] = menuItem;
            }
        });
    }
}