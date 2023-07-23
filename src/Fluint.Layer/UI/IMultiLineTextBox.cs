//
// IMultiLineTextBox.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.UI;

public interface IMultiLineTextBox : IModule, IGuiComponent
{
    string Text
    {
        get;
        set;
    }
}