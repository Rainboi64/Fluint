﻿//
// VSync.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Miscellaneous;
using Fluint.Layer.SDK;
using Fluint.Layer.UI;

namespace Fluint.Engine.GL46.Commands
{
    [Module("VSync Toggle Command", "Toggles the virtual sync of the main window", "type the command to toggle vsync")]
    public class VSync : ICommand
    {
        public string Command { get => "vsync"; }
        private readonly ModulePacket _packet;
        private readonly ILogger _logger;

        public VSync(ModulePacket packet, ILogger logger)
        {
            _packet = packet;
            _logger = logger;
        }

        public void Do(string[] args)
        {
            var window =  _packet.GetSingleton<IGuiInstanceManager>().MainWindow;
            if (window is null)
            {
                _logger.Error("[{0}] Couldn't Find Window; Instance Manager: {1}", "OpenGL46", _packet.GetSingleton<IGuiInstanceManager>().MainWindow
                );
                return;
            }

            window.VSync = !window.VSync;

            // this is inverted because the event happens after the next frame.
            if (!window.VSync)
            {
                ConsoleHelper.WriteEmbeddedColorLine("VSync is [green]ON[/green]");
            }
            else
            {
                ConsoleHelper.WriteEmbeddedColorLine("VSync is [red]OFF[/red]");
            }
        }
    }
}
