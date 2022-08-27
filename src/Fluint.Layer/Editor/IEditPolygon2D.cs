// 
// IEditBox2D.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Editor;

public interface IEditPolygon2D
{
    void Load(Vector3[] points);
    void Update();
    void Render();
}