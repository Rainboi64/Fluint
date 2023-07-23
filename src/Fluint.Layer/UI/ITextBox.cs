//
// ITextBox.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface ITextBox : IModule, IGuiComponent
{
    string Text
    {
        get;
        set;
    }

    string SideText
    {
        get;
        set;
    }
}