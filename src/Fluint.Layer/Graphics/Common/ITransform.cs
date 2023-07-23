// 
// Transform.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.Common;

[Initialization(InitializationMethod.Scoped)]
public interface ITransform : IComponent
{
    public Vector3 Translation
    {
        get;
        set;
    }

    public Vector3 EulerAngles
    {
        get;
        set;
    }

    public Vector3 Scale
    {
        get;
        set;
    }

    public Matrix GetModelMatrix();
}