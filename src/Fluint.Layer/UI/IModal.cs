// 
// IModal.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IModal : IGuiContainer<IGuiComponent>, IModule
{
    string Title
    {
        get;
        set;
    }

    bool Open
    {
        get;
        set;
    }
}