// 
// ISketchSystem.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor.Tools.Sketching;

[Initialization(InitializationMethod.Scoped)]
public interface ISketchSystem : ISystem<ISketch>
{
    IReadOnlyCollection<ISketch> Sketches
    {
        get;
    }

    bool Pick(Ray ray, out ISketch sketch);

    Vector3[] GetVertices();
}