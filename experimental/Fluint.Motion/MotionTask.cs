using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;
using Fluint.Layer.Tasks;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Motion;

public class MotionTask : ITask
{
    private readonly ModulePacket _packet;

    public MotionTask(ModulePacket packet)
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
            menuItem.Text = "Motion";
            menuItem.OnClick = new ModularAction {
                () => window?.SpawnControl<MotionWindow>()
            };
            if (mainMenu != null)
            {
                mainMenu["Motion"] = menuItem;
            }
        });
    }
}