//
// Font.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.UI;

public class Font
{
    public Font(Action enable, Action disable)
    {
        EnableFont = enable;
        DisableFont = disable;
    }

    public Action EnableFont
    {
        get;
    }

    public Action DisableFont
    {
        get;
    }
}