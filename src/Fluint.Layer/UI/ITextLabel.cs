//
// ITextLabel.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface ITextLabel : IModule, IGuiComponent
{
    string Text
    {
        get;
        set;
    }
}