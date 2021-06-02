using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using ILogger = Fluint.Layer.Diagnostics.ILogger;

namespace Fluint.Implementation.Diagnostics
{
    public class SerilogLogger : ILogger
    {
        public SerilogLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("./logs/log-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Information(string messageTemplate)
        {
            Log.Information(messageTemplate);
        }

        public void Information(string messageTemplate, params object[] propertyValue)
        {
            Log.Information(messageTemplate, propertyValue);
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

        public void Error(string messageTemplate)
        {
            Log.Error(messageTemplate);
        }

        public void Error(string messageTemplate, params object[] propertyValue)
        {
            Log.Error(messageTemplate, propertyValue);
        }

        public void Error(Exception exception, string messageTemplate)
        {
            Log.Error(exception, messageTemplate);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValue)
        {
            Log.Error(exception, messageTemplate, propertyValue);
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
    }
}
