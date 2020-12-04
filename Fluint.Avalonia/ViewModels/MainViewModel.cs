using System;
using System.Collections.Generic;
using System.Text;
using Dock.Model;
using Dock.Model.Controls;

namespace Fluint.Avalonia.ViewModels
{
    public class MainViewModel : RootDock
    {
        public override IDockable Clone()
        {
            var mainViewModel = new MainViewModel();

            CloneHelper.CloneDockProperties(this, mainViewModel);
            CloneHelper.CloneRootDockProperties(this, mainViewModel);

            return mainViewModel;
        }
    }
}
