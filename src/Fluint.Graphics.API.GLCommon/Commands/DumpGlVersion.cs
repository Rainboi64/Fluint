// 
// GlVersion.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.SDK;
using Fluint.Layer.UI;
using OpenTK.Graphics.OpenGL;

namespace Fluint.Graphics.API.GLCommon.Commands;

[Module("Dump opengl information", "prints opengl runtime data to information buffer",
    "prints opengl runtime data to information buffer")]
public class DumpGlVersion : ICommand
{
    private readonly ModulePacket _packet;

    public DumpGlVersion(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "glversion";

    public void Do(string[] args)
    {
        var logger = _packet.GetSingleton<ILogger>();

        var window = _packet.GetSingleton<IGuiInstanceManager>().MainWindow;
        if (window is null)
        {
            logger.Error("[{0}] Couldn't Find Window; Instance Manager: {1}", "OpenGLCommon",
                _packet.GetSingleton<IGuiInstanceManager>().MainWindow
            );
            return;
        }

        window.Enqueue(() => {
            logger.Information("[{0}] OpenGL Version: {1}", "OpenGLCommon", GL.GetString(StringName.Version));
            logger.Information("[{0}] Version: {1}", "OpenGLCommon", GL.GetString(StringName.ShadingLanguageVersion));
            logger.Information("[{0}] Renderer: {1}", "OpenGLCommon", GL.GetString(StringName.Renderer));
            logger.Information("[{0}] Vendor: {1}", "OpenGLCommon", GL.GetString(StringName.Vendor));
            logger.Information("[{0}] Extensions: {1}", "OpenGLCommon", GL.GetString(StringName.Extensions));
        });
    }
}