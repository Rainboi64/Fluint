using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Fluint.SDK.Commands.Splitter.GUI
{
    internal class MainWindow : Window
    {

        private TextBlock _typeTile;
        private IEnumerable<Type> _loadedTypes;
        public MainWindow()
        {
            InitializeComponent();
            _typeTile = this.FindControl<TextBlock>("TypeTitle");
            var openMenuItem = this.FindControl<MenuItem>("OpenMenuItem");
            openMenuItem.Click += OpenMenuItem_Click;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OpenMenuItem_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var AssemblyTypes = this.FindControl<ListBox>("Namespaces");
            InitDll(@".\modules\Fluint.Engine.GL46.dll");
            AssemblyTypes.Items = GetAssemblyNamespaces();
        }

        private void InitDll(string fileName)
        {
            _loadedTypes = Assembly.LoadFile(Path.GetFullPath(fileName)).GetTypes();
        }

        private IEnumerable<TextBlock> GetAssemblyNamespaces()
        {
            var namespaces = new List<string>();
            foreach (var type in _loadedTypes)
            {
                if (!namespaces.Contains(type.Namespace))
                {                    
                    TextBlock item = new TextBlock
                    {
                        Name = $"MenuItem.{ type.Namespace }",
                        Text = type.Namespace,
                        Tag = type,
                    };
                    item.PointerPressed += Item_Clicked;
                    namespaces.Add(type.Namespace);
                    yield return item;
                }
            }
        }

        private void Item_Clicked(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            var typeInfo = (TypeInfo)((TextBlock)sender).Tag;
            _typeTile.Text = typeInfo.Name;
            Console.WriteLine(typeInfo.Namespace);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
