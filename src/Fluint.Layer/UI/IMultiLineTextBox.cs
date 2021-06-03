//
// IMultiLineTextBox.cs
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
    public interface IMultiLineTextBox : IModule, IGuiComponent
    { 
        string Text { get; set; }
    }
}
