//
// IGuiComponent.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI
{
    public interface IGuiComponent
    {
        string Name { get; }
        void Begin(string name);
        void Tick();
        ICollection<IGuiComponent> Children { get; }
    }
}
