// 
// ToolboxControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Localization;
using Fluint.Layer.Mathematics;
using Fluint.Layer.StateManagement;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout.Base.Controls;

public class ToolboxControl : Control
{
    private readonly Color4 _active;
    private readonly Dictionary<ITool, IImageButton> _buttons = new();
    private readonly Color4 _inactive;
    private readonly ToolState _state;

    public ToolboxControl(ModulePacket packet, ILocalizationManager localizationManager,
        IConfigurationManager configManager, IStateManager stateManager)
    {
        var tools = packet.GetInstances<ITool>();
        var theme = configManager.Get<ThemeConfiguration>();

        _active = new Color4(theme.ButtonActive.X, theme.ButtonActive.Y, theme.ButtonActive.Z, theme.ButtonActive.W);
        _inactive = new Color4(theme.Button.X, theme.Button.Y, theme.Button.Z, theme.Button.W);

        _state = stateManager.GetState<ToolState>();

        var container = packet.CreateScoped<IContainer>();
        container.Title = localizationManager.Fetch("toolbox");
        container.Resizable = false;

        foreach (var tool in tools)
        {
            var type = tool.GetType();
            var toolAttribute = type.GetCustomAttributes(true).OfType<ToolAttribute>().FirstOrDefault();

            if (toolAttribute is null)
            {
                continue;
            }

            var button = packet.CreateScoped<IImageButton>();
            button.Text = toolAttribute.DisplayName;
            button.Size = new Vector2i(48, 48);
            button.Padding = 0;
            button.Path = toolAttribute.IconPath;
            button.OnClick = () => {
                _state.ActiveTool = tool;
            };

            _buttons[tool] = button;

            container.Add(type.Name, button);
        }

        Children.Add(container);
    }

    public override void Tick()
    {
        foreach (var pair in _buttons)
        {
            pair.Value.BackgroundColor = pair.Key == _state.ActiveTool ? _active : _inactive;
        }

        base.Tick();
    }
}