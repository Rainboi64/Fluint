﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Dock.Model;
using Fluint.Avalonia.Models;
using Fluint.Avalonia.ViewModels;
using Fluint.Avalonia.Views;

namespace Fluint.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var factory = new MainDockFactory(new DemoData());
            var layout = factory.CreateLayout();
            factory.InitLayout(layout);

            var mainWindowViewModel = new MainWindowViewModel()
            {
                Factory = factory,
                Layout = layout
            };

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {

                var mainWindow = new MainWindow
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
