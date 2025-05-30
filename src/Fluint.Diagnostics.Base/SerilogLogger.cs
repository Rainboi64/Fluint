//
// SerilogLogger.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.IO;
using System.Reflection;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Serilog;
using Serilog.Core.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Fluint.Layer.Diagnostics.ILogger;

namespace Fluint.Diagnostics.Base;

public class SerilogLogger : ILogger
{
    private INotificationProvider _notificationsProvider;
    private ModulePacket _packet;
    public SerilogLogger(ModulePacket packet)
    {
        _packet = packet;
        
        var exePath = Path.GetDirectoryName(
            Assembly.GetEntryAssembly()?.Location);

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With(new PropertyEnricher("Caller", Assembly.GetCallingAssembly()))
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .WriteTo.File(exePath + "/logs/log-.log", rollingInterval: RollingInterval.Day)
            .WriteTo.Conditional( (log) =>
            {
                NotificationType level;
                switch (log.Level)
                {
                    case LogEventLevel.Error:
                    case LogEventLevel.Fatal:
                        level = NotificationType.Error;
                        break;
                    default:
                    case LogEventLevel.Warning:
                        level = NotificationType.Warning;
                        break;

                    case LogEventLevel.Debug:
                        level = NotificationType.Debug;
                        break;
                    case LogEventLevel.Verbose:
                    case LogEventLevel.Information:
                        level = NotificationType.Information;
                        break;
                }
                GetProvider().AddNotification(level, "Message", string.Format( log.RenderMessage(), "Hello"));
                return true;
            }, (config) =>
            {
                
            })
            .CreateLogger();
    }

    private INotificationProvider GetProvider()
    {
        return _notificationsProvider ??= _packet.GetSingleton<INotificationProvider>() ?? new DummyProvider();
    }

    public void Information(string messageTemplate)
    {
        Log.Information(messageTemplate);
        GetProvider().AddNotification(new Notification(NotificationType.Information, "", messageTemplate));
    }

    public void Information(string messageTemplate, params object[] propertyValue)
    {
        Log.Information(messageTemplate, propertyValue);
        GetProvider().AddNotification(new Notification(NotificationType.Information, "", string.Format(messageTemplate, propertyValue)));
    }

    public void Information(string messageTemplate, object first)
    {
        Log.Information(messageTemplate, first);
        GetProvider().AddNotification(new Notification(NotificationType.Information, "", string.Format(messageTemplate, first)));
    }

    public void Information(string messageTemplate, object first, object second)
    {
        Log.Information(messageTemplate, first, second);
    }

    public void Information(string messageTemplate, object first, object second, object third)
    {
        Log.Information(messageTemplate, first, second, third);
    }

    public void Information(Exception exception, string messageTemplate)
    {
        Log.Information(exception, messageTemplate);

    }

    public void Information(Exception exception, string messageTemplate, params object[] propertyValue)
    {
        Log.Information(exception, messageTemplate, propertyValue);
    }

    public void Debug(string messageTemplate)
    {
        Log.Debug(messageTemplate);
    }

    public void Debug(string messageTemplate, params object[] propertyValue)
    {
        Log.Debug(messageTemplate, propertyValue);
    }

    public void Debug(Exception exception, string messageTemplate)
    {
        Log.Debug(exception, messageTemplate);
    }

    public void Debug(Exception exception, string messageTemplate, params object[] propertyValue)
    {
        Log.Debug(exception, messageTemplate, propertyValue);
    }

    public void Warning(string messageTemplate)
    {
        Log.Warning(messageTemplate);
    }

    public void Warning(string messageTemplate, params object[] propertyValue)
    {
        Log.Warning(messageTemplate, propertyValue);
    }

    public void Warning(Exception exception, string messageTemplate)
    {
        Log.Warning(exception, messageTemplate);
    }

    public void Warning(Exception exception, string messageTemplate, params object[] propertyValue)
    {
        Log.Warning(exception, messageTemplate, propertyValue);
    }

    public void Warning(string messageTemplate, object first)
    {
        Log.Warning(messageTemplate, first);
    }

    public void Warning(string messageTemplate, object first, object second)
    {
        Log.Warning(messageTemplate, first, second);
    }

    public void Warning(string messageTemplate, object first, object second, object third)
    {
        Log.Warning(messageTemplate, first, second, third);
    }

    public void Error(string messageTemplate)
    {
        Log.Error(messageTemplate);
        GetProvider().AddNotification(new Notification(NotificationType.Error, "", messageTemplate));
    }

    public void Error(string messageTemplate, params object[] propertyValue)
    {
        Log.Error(messageTemplate, propertyValue);   
        GetProvider().AddNotification(new Notification(NotificationType.Error, "", string.Format(messageTemplate, propertyValue)));
    }

    public void Error(Exception exception, string messageTemplate)
    {
        Log.Error(exception, messageTemplate);
        GetProvider().AddNotification(new Notification(NotificationType.Error, "", messageTemplate));
    }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValue)
    {
        Log.Error(exception, messageTemplate, propertyValue);
    }

    public void Error(string messageTemplate, object first)
    {
        Log.Error(messageTemplate, first);
    }

    public void Error(string messageTemplate, object first, object second)
    {
        Log.Error(messageTemplate, first, second);
    }

    public void Error(string messageTemplate, object first, object second, object third)
    {
        Log.Error(messageTemplate, first, second, third);
    }

    public void Verbose(string messageTemplate)
    {
        Log.Verbose(messageTemplate);
    }

    public void Verbose(string messageTemplate, params object[] propertyValue)
    {
        Log.Verbose(messageTemplate, propertyValue);
    }

    public void Verbose(Exception exception, string messageTemplate)
    {
        Log.Verbose(exception, messageTemplate);
    }

    public void Verbose(Exception exception, string messageTemplate, params object[] propertyValue)
    {
        Log.Verbose(exception, messageTemplate, propertyValue);
    }

    public void Verbose(string messageTemplate, object first)
    {
        Log.Verbose(messageTemplate, first);
    }

    public void Verbose(string messageTemplate, object first, object second)
    {
        Log.Verbose(messageTemplate, first, second);
    }

    public void Verbose(string messageTemplate, object first, object second, object third)
    {
        Log.Verbose(messageTemplate, first, second, third);
    }

    public void Fatal(string messageTemplate)
    {
        Log.Fatal(messageTemplate);
    }

    public void Fatal(string messageTemplate, params object[] propertyValue)
    {
        Log.Fatal(messageTemplate, propertyValue);
    }

    public void Fatal(Exception exception, string messageTemplate)
    {
        Log.Fatal(exception, messageTemplate);
    }

    public void Fatal(Exception exception, string messageTemplate, params object[] propertyValue)
    {
        Log.Fatal(exception, messageTemplate, propertyValue);
    }

    public void Fatal(string messageTemplate, object first)
    {
        throw new NotImplementedException();
    }

    public void Fatal(string messageTemplate, object first, object second)
    {
        throw new NotImplementedException();
    }

    public void Fatal(string messageTemplate, object first, object second, object third)
    {
        throw new NotImplementedException();
    }
}