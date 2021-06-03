//
// IFontManager.cs
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
    public interface IFontManager : IModule
    {
        Font LoadFont(string fontName, float fontSize = 16.0f);
    }
}
