/// <summary>
/// Greatly Inspired by https://github.com/varon/GLWpfControl/blob/master/src/GLWpfControl/GLWpfControl.cs;
/// </summary>

using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Threading;
using Avalonia.Markup.Xaml;
using Fluint.Layer.Graphics;
using System;
using Avalonia.Rendering;
using Avalonia.Media;
using System.Diagnostics;

namespace Fluint.Avalonia.Controls
{

    public class RendererContext : NativeControlHost, IDisposable
    {
        private IntPtr _handle;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private TimeSpan _lastFrameStamp;

        private bool _contextInit = false;

        /// <summary>
        /// The native graphics api context
        /// </summary>
        public IBindingContext BindingContext;

        /// <summary>
        /// Gets called when initialization is finished.
        /// </summary>
        public event Action Ready;

        /// <summary>
        /// Main Render Event (gets called when rendering is requested, should contain the actual rendering data)
        /// </summary>
        public event Action<TimeSpan> RenderCallback;

        public RendererContext()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void Load(IBindingContext bindingContext)
        {
            BindingContext = bindingContext;
        }
        
        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
        {
            var handle = base.CreateNativeControlCore(parent);
            _handle = handle.Handle;

            if (!_contextInit)
            {
                var settings = new BindingContextSettings(_handle, (int)Math.Truncate(Height), (int)Math.Truncate(Width));
                BindingContext.InitializeContext(settings);

                Ready?.Invoke();

                _contextInit = true;
            }

            return handle;
        }

        public override void Render(DrawingContext context)
        {
            var curFrameStamp = _stopwatch.Elapsed;
            var deltaT = curFrameStamp - _lastFrameStamp;
            _lastFrameStamp = curFrameStamp;

            BindingContext.MakeCurrent();

            BindingContext.Resize((int)this.Bounds.Width, (int)this.Bounds.Height);

            BindingContext.PreRender();
            RenderCallback?.Invoke(deltaT);
            BindingContext.PostRender();

            BindingContext.SwapBuffers();

            base.Render(context);

            // Re-queue render call in the UIThread. causes this to loop
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }

        public void Dispose()
        {
            BindingContext.Dispose();
        }
    }
}
