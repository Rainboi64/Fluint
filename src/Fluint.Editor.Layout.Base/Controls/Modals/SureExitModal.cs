// 
// SureExitModal.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Functionality;
using Fluint.Layer.Functionality.Common;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout.Base.Controls.Modals;

public class SureExitModal : Control
{
    public SureExitModal(ModulePacket packet, ILocalizationManager localizationManager, IActionManager actionManager)
    {
        var messageBox = packet.CreateScoped<IMessageBox>();

        messageBox.Open = true;

        messageBox.Buttons = MessageBoxButton.OkCancel;
        messageBox.Title = localizationManager.Fetch("exit_confirmation_title");
        messageBox.Prompt = localizationManager.Fetch("exit_confirmation_message");

        messageBox.OnClick = new ModularAction
        {
            () =>
            {
                if (messageBox.Result == MessageBoxResult.Ok)
                {
                    actionManager.GetAction<IGracefulExit>().Exit();
                }
            }
        };

        Children.Add(messageBox);
    }
}