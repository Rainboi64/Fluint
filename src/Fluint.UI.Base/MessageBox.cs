// 
// MessageBox.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using Fluint.Layer.Functionality;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;
using ImGuiNET;

namespace Fluint.UI.Base;

public class MessageBox : IMessageBox
{
    private readonly ILocalizationManager _localizationManager;
    private bool _open;

    public MessageBox(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;
    }

    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
    }

    public void Tick()
    {
        if (_open)
        {
            ImGui.OpenPopup($"{Title}###{Name}");
        }

        if (!ImGui.BeginPopupModal($"{Title}###{Name}", ref _open))
        {
            return;
        }

        ImGui.TextWrapped(Prompt);

        switch (Buttons)
        {
            case MessageBoxButton.Ok:
            {
                if (ImGui.Button(_localizationManager.Fetch("ok")))
                {
                    Result = MessageBoxResult.Ok;
                    OnClick?.Invoke();
                    _open = false;
                }

                break;
            }
            case MessageBoxButton.OkCancel:
            {
                if (ImGui.Button(_localizationManager.Fetch("ok")))
                {
                    Result = MessageBoxResult.Ok;
                    OnClick?.Invoke();
                    _open = false;
                }

                ImGui.SameLine();
                if (ImGui.Button(_localizationManager.Fetch("cancel")))
                {
                    Result = MessageBoxResult.Cancel;
                    OnClick?.Invoke();
                    _open = false;
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public MessageBoxButton Buttons
    {
        get;
        set;
    }

    public MessageBoxResult Result
    {
        get;
        private set;
    }

    public string Title
    {
        get;
        set;
    }

    public string Prompt
    {
        get;
        set;
    }

    public bool Open
    {
        get => _open;
        set => _open = value;
    }

    public ModularAction OnClick
    {
        get;
        set;
    }
}