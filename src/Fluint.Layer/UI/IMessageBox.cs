// 
// IMessageBox.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Functionality;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IMessageBox : IGuiComponent, IModule
{
    MessageBoxButton Buttons
    {
        get;
        set;
    }

    MessageBoxResult Result
    {
        get;
    }

    string Title
    {
        get;
        set;
    }

    string Prompt
    {
        get;
        set;
    }

    bool Open
    {
        get;
        set;
    }

    ModularAction OnClick
    {
        get;
        set;
    }
}