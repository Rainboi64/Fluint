// 
// Transform.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Mathematics;

public class Transform
{
    public Transform()
    {
        Translation = Vector3.Zero;
        EulerAngles = Vector3.Zero;
        Scale = Vector3.One;
    }

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

    public Matrix GetModelMatrix()
    {
        return Matrix.Scaling(Scale) * Matrix.RotationYawPitchRoll(EulerAngles.X, EulerAngles.Y, EulerAngles.Z) *
               Matrix.Translation(Translation);
    }
}