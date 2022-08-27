// 
// IGenerateSphere.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Functionality.Graphics;

public interface IGenerateSphere : ILogicModule
{
    public PositionColorVertex[] GenerateUVSphere();
    public PositionColorVertex[] GenerateQuadSphere();
    public PositionColorVertex[] GenerateIcoSphere();
}