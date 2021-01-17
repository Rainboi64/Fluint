using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Fluint.Avalonia.Controls
{
    public class Ribbon : UserControl
    {
        public Ribbon()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
