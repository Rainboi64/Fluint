// 
// ActionManager.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;

namespace Fluint.Logic.Base;

public class ActionManager : IActionManager
{
    private readonly ModulePacket _packet;

    public ActionManager(ModulePacket packet)
    {
        _packet = packet;
    }

    public T GetAction<T>() where T : ILogicModule
    {
        return _packet.GetInstances<T>().FirstOrDefault();
    }
}