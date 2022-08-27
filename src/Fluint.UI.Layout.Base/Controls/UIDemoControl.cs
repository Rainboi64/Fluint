// 
// UIDemoControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.UI;

namespace Fluint.UI.Layout.Base.Controls;

public class UIDemoControl : Control
{
    public UIDemoControl(ModulePacket packet)
    {
        Children.Add(packet.CreateScoped<IDemo>());
    }
}