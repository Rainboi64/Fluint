using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Fluint.Avalonia.Views
{
    public class RecentProjectView : Window
    {
        public RecentProjectView()
        {
            this.InitializeComponent();
#if DEBUG
           // this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
