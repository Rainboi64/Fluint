using System;
using Fluint.Layer.Miscellaneous;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Fluint.Avalonia.Controls;
using Fluint.Avalonia.ViewModels;

namespace Fluint.Avalonia.Views
{
    // MVVM Research needed
    public class RendererDocumentView : UserControl
    {
        private readonly RendererContext _rendererContext;
        private RendererDocumentViewModel _viewModel;

        public RendererDocumentView()
        {
            this.InitializeComponent();
            _rendererContext = this.FindControl<RendererContext>("renderer");
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            // TODO: Add window caching.
            // TODO: probably should implement this into a logger.
            ConsoleHelper.WriteEmbeddedColorLine($"Inititialized [cyan]{this}[/cyan]");
            _viewModel = (RendererDocumentViewModel)DataContext;
            _rendererContext.Load(_viewModel.BindingContext, _viewModel.TaskManager);

            _rendererContext.Ready += ContextReady;
            _rendererContext.RenderCallback += ContextRender;            
        }

        private void ContextReady()
        {
            _viewModel.Ready();
        }

        private void ContextRender(TimeSpan obj)
        {
            _viewModel.Render();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
