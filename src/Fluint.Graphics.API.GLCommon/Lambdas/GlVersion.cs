// 
// GlVersion.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Linq;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;
using Fluint.Layer.UI;
using OpenTK.Graphics.OpenGL;

namespace Fluint.Graphics.API.GLCommon.Lambdas;

[Module("Dump opengl information", "prints opengl runtime data to information buffer",
    "prints opengl runtime data to information buffer")]
public class GlVersion : ILambda
{
    private readonly ModulePacket _packet;

    public GlVersion(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "glversion";

    public LambdaObject Run(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();

        var window = _packet.GetSingleton<IGuiInstanceManager>().Windows.FirstOrDefault();
        if (window is null)
        {
            return LambdaObject.Error("Couldn't find window object");
        }

        window.Enqueue(() =>
        {
            logger.Information("[{0}] OpenGL Version: {1}", "OpenGLCommon", GL.GetString(StringName.Version));
            logger.Information("[{0}] Version: {1}", "OpenGLCommon", GL.GetString(StringName.ShadingLanguageVersion));
            logger.Information("[{0}] Renderer: {1}", "OpenGLCommon", GL.GetString(StringName.Renderer));
            logger.Information("[{0}] Vendor: {1}", "OpenGLCommon", GL.GetString(StringName.Vendor));
            logger.Information("[{0}] Extensions: {1}", "OpenGLCommon", GL.GetString(StringName.Extensions));
        });

        return LambdaObject.Success;
    }
}