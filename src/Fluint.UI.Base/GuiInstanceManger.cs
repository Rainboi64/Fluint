//
// GuiInstanceManger.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.SDK.Base.UI
{
    public class GuiInstanceManger : IGuiInstanceManager
    {
        public IWindow MainWindow
        {
            get;
            private set;
        }

        public void Adopt(in IWindow window)
        {
            MainWindow = window;
        }
    }
}