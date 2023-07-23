//
// IGuiInstanceManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Singleton)]
public interface IGuiInstanceManager : IModule
{
    IReadOnlyCollection<IWindow> Windows
    {
        get;
    }

    /// <summary>
    ///     please don't call outside provider.
    /// </summary>
    /// <param name="window"></param>
    void Adopt(in IWindow window);
}