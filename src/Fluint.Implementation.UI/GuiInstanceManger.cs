using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.Implementation.UI
{
    public class GuiInstanceManger : IGuiInstanceManager
    {
        public IWindow MainWindow { get; private set; }
        public void Adopt(in IWindow window)
        {
            MainWindow = window;
        }
    }
}
