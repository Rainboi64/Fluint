//
// IContainer.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IContainer : IModule, IGuiContainer<IGuiComponent>
    {
        string Title { get; set; }
        bool IsFocused
        {
            get;
        }
        Vector2i Size
        {
            get;
        }
    }
}
