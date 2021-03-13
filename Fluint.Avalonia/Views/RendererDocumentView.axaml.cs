using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Fluint.Avalonia.Controls;
using Fluint.Avalonia.ViewModels;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Avalonia.Views
{
    public class RendererDocumentView : UserControl
    {
        private readonly RendererContext _rendererContext;
        private readonly ModulePacket _packet;

        public RendererDocumentView(ModulePacket packet)
        {
            this.InitializeComponent();
            _packet = packet;
            _rendererContext = this.FindControl<RendererContext>("renderer");
            _rendererContext.Load(_packet.New<IBindingContext>());

            _rendererContext.Ready += ContextReady;
            _rendererContext.RenderCallback += ContextRender;
        }

        public RendererDocumentView() { }

        private void ContextReady()
        {

        }

        private void ContextRender(TimeSpan obj)
        {

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
