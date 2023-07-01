// 
// Transform.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.Common;

namespace Fluint.Graphics.Base;

public class Transform : Layer.Mathematics.Transform, ITransform
{
    public IEntity Entity
    {
        get;
        set;
    }
}