//
// ILogger.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Diagnostics;

/// <summary>
///     an interface for a Logger
/// </summary>
[Initialization(InitializationMethod.Singleton)]
public interface ILogger : IModule
{
    void Information(string messageTemplate);
    void Information(string messageTemplate, params object[] propertyValue);
    void Information(string messageTemplate, object first);
    void Information(string messageTemplate, object first, object second);
    void Information(string messageTemplate, object first, object second, object third);
    void Information(Exception exception, string messageTemplate);
    void Information(Exception exception, string messageTemplate, params object[] propertyValue);
    void Debug(string messageTemplate);
    void Debug(string messageTemplate, params object[] propertyValue);
    void Debug(Exception exception, string messageTemplate);
    void Debug(Exception exception, string messageTemplate, params object[] propertyValue);
    void Warning(string messageTemplate);
    void Warning(string messageTemplate, params object[] propertyValue);
    void Warning(Exception exception, string messageTemplate);
    void Warning(Exception exception, string messageTemplate, params object[] propertyValue);
    void Warning(string messageTemplate, object first);
    void Warning(string messageTemplate, object first, object second);
    void Warning(string messageTemplate, object first, object second, object third);
    void Error(string messageTemplate);
    void Error(string messageTemplate, params object[] propertyValue);
    void Error(Exception exception, string messageTemplate);
    void Error(Exception exception, string messageTemplate, params object[] propertyValue);
    void Error(string messageTemplate, object first);
    void Error(string messageTemplate, object first, object second);
    void Error(string messageTemplate, object first, object second, object third);
    void Verbose(string messageTemplate);
    void Verbose(string messageTemplate, params object[] propertyValue);
    void Verbose(Exception exception, string messageTemplate);
    void Verbose(Exception exception, string messageTemplate, params object[] propertyValue);
    void Verbose(string messageTemplate, object first);
    void Verbose(string messageTemplate, object first, object second);
    void Verbose(string messageTemplate, object first, object second, object third);
    void Fatal(string messageTemplate);
    void Fatal(string messageTemplate, params object[] propertyValue);
    void Fatal(Exception exception, string messageTemplate);
    void Fatal(Exception exception, string messageTemplate, params object[] propertyValue);
    void Fatal(string messageTemplate, object first);
    void Fatal(string messageTemplate, object first, object second);
    void Fatal(string messageTemplate, object first, object second, object third);
}