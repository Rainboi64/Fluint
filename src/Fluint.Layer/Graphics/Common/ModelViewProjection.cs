// 
// ModelViewProjection.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System.Runtime.InteropServices;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics.Common;

[StructLayout(LayoutKind.Sequential)]
public struct ModelViewProjection
{
    public ModelViewProjection(Matrix model, Matrix view, Matrix projection)
    {
        Model = model;
        View = view;
        Projection = projection;
    }

    public readonly Matrix Model;
    public readonly Matrix View;
    public readonly Matrix Projection;
}