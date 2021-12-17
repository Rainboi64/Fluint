//
// ITextLabel.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Layer.UI
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITextLabel : IModule, IGuiComponent
    {
        string Text { get; set; }
    }
}
