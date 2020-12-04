using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Fluint.Avalonia.Views
{
    public class RendererDocumentView : UserControl
    {
        public RendererDocumentView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
