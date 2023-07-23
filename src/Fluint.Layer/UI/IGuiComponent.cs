//
// IGuiComponent.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.UI;

public interface IGuiComponent
{
    string Name
    {
        get;
    }

    void Begin(string name);
    void Tick();
}