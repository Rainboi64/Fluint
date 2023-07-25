// 
// Spline.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Editor.Tools.Sketching.Shapes;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching.Shapes;

public class Spline : ISpline
{
    public Vector3 Start
    {
        get;
        set;
    }

    public Vector3 End
    {
        get;
        set;
    }

    public BoundingBox BoundingBox => BoundingBox.FromPoints(new[] { Start, End });
    public Vector3[] Vertices => new[] { Start, End };

    public IEntity Entity
    {
        get;
        set;
    }
}