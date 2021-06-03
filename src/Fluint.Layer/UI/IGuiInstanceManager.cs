//
// IGuiInstanceManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Singleton)]
    public interface IGuiInstanceManager : IModule
    {
        IWindow MainWindow { get; }

        /// <summary>
        /// please don't call outside provider.
        /// </summary>
        /// <param name="window"></param>
        void Adopt(in IWindow window);
    }
}
