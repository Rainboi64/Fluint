﻿using System;
using System.Collections.Generic;
using System.Text;
using Dock.Model.Controls;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;

namespace Fluint.Avalonia.ViewModels
{
    class RendererDocumentViewModel : Document
    {
        public RendererDocumentViewModel(ModulePacket packet)
        {
            BindingContext = packet.GetScoped<IBindingContext>();
        }

        public IBindingContext BindingContext { get; }
    }
}