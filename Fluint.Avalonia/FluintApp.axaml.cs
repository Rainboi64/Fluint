using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Dock.Model;
using Fluint.Avalonia.Models;
using Fluint.Avalonia.ViewModels;
using Fluint.Avalonia.Views;
using Fluint.Layer;
using Fluint.Layer.DependencyInjection;
using System;

namespace Fluint.Avalonia
{
    public class App : Application
    {
        private ModulePacket _packet;

        public override void Initialize()
        {
            ModulesManager modulesManager = new ModulesManager();
            Console.WriteLine("Loading './modules'");
            modulesManager.LoadFolder("./modules");

            _packet = modulesManager.ModuleCollection.ModulePacket;

            AvaloniaXamlLoader.Load(this);

            DataTemplates.Add(new ViewLocator(_packet));
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var factory = new MainDockFactory(new object(), _packet);
            var layout = factory.CreateLayout();
            factory.InitLayout(layout);

            var mainWindowViewModel = new MainWindowViewModel()
            {
                Factory = factory,
                Layout = layout
            };

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {

                var mainWindow = new MainWindow()
                {
                    DataContext = mainWindowViewModel
                };

                mainWindow.Closing += (sender, e) =>
                {
                    if (layout is IDock dock)
                    {
                        dock.Close();
                    }
                };

                desktopLifetime.MainWindow = mainWindow;

                desktopLifetime.Exit += (sennder, e) =>
                {
                    if (layout is IDock dock)
                    {
                        dock.Close();
                    }
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
            {
                var mainView = new MainView()
                {
                    DataContext = mainWindowViewModel
                };

                singleViewLifetime.MainView = mainView;
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
