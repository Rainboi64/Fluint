// 
// IMeshFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Graphics;

public interface IMeshFactory
{
    IMesh CreateMesh();
    IMesh CreateUnitBox();
    IMesh CreateUnitSphere();
}