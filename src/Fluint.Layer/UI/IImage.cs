// 
// IImage.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IImage : IModule, IGuiComponent
{
    Vector2i Size
    {
        get;
        set;
    }

    string Path
    {
        get;
        set;
    }
}