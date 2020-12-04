using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Markup.Xaml;
using Fluint.Layer.Graphics;
using System;

namespace Fluint.Avalonia.Controls
{
    public class RendererContext : NativeControlHost, IRendererContext
    {
        public RendererContext()
        {
            InitializeComponent();
        }

        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
        {
            var handle = base.CreateNativeControlCore(parent);
            Handle = handle.Handle;
            return handle;
        }

        public IntPtr Handle { get; private set; }
        double IRendererContext.Height => Bounds.Height;
        double IRendererContext.Width => Bounds.Height;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
