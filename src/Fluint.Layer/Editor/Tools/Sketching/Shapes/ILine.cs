// 
// ISpline.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Tools.Sketching.Shapes;

[Initialization(InitializationMethod.Scoped)]
public interface ISpline : ISketch, IModule
{
    Vector3 Start
    {
        get;
        set;
    }

    Vector3 End
    {
        get;
        set;
    }
}