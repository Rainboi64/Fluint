// 
// CameraConfiguration.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Configuration;
using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Editor.Viewport;

public class CameraConfiguration : IConfiguration
{
    public float MinDepth
    {
        get;
        set;
    } = 0.1f;

    public float MaxDepth
    {
        get;
        set;
    } = 1000f;

    public ProjectionMode ProjectionMode
    {
        get;
        set;
    } = ProjectionMode.Orthogonal;

    public float Zoom
    {
        get;
        set;
    } = 0.01f;

    public float FOV
    {
        get;
        set;
    } = 90;
}