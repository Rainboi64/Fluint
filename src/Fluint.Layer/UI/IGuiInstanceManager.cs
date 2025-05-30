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
    
    
    void Adopt(in IWindow window);
}