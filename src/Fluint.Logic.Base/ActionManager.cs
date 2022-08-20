// 
// ActionManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;

namespace Fluint.Logic.Base;

public class ActionManager : IActionManager
{
    private readonly List<ILogicModule> _modules;

    public ActionManager(ModulePacket packet)
    {
        _modules = packet.GetInstances<ILogicModule>().ToList();
    }
    
    public T GetAction<T>() where T : ILogicModule
    {
        return _modules.OfType<T>().First();
    }
}