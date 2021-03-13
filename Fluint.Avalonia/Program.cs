using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Fluint.Layer.Miscellaneous;
using System.Reflection;

namespace Fluint.Avalonia
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            ConsoleHelper.WriteEmbeddedColorLine($"\n[red]Welcome to Fluint.[/red]\nStart-line called at {DateTime.Now} Called by {Assembly.GetCallingAssembly()}\nRunning in {Assembly.GetExecutingAssembly()}\n");
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }


        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

    }
}
