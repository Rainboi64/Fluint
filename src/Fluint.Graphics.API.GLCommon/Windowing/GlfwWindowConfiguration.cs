// 
// GLFWWindowConfiguration.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Configuration;

namespace Fluint.Graphics.API.GLCommon.Windowing;

[Configuration("Windowing")]
public class GlfwWindowConfiguration : IConfiguration
{
    public GlfwWindowConfiguration()
    {
        MajorOpenGLVersion = 4;
        MinorOpenGLVersion = 1;
        NumberOfSamples = 4;
    }

    public int NumberOfSamples
    {
        get;
        set;
    }

    public int MinorOpenGLVersion
    {
        get;
        set;
    }

    public int MajorOpenGLVersion
    {
        get;
        set;
    }
}