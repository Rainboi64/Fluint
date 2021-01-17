using System;

namespace Fluint.Layer.Debugging
{
    /// <summary>
    /// an interface for a Logger
    /// </summary>
    [Initialization(InitializationMethod.Singleton)]
    public interface ILogger : IModule
    {
        /// <summary>
        /// For logging general information.
        /// </summary>
        /// <param name="messageTemplate">Information For Logging</param>
        void Information(string messageTemplate);

        /// <summary>
        /// For logging general information, with extra data.
        /// </summary>
        /// <param name="messageTemplate">Main message</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Information(string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging information about an exception.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information.</param>
        void Information(Exception exception, string messageTemplate);

        /// <summary>
        /// For logging information about an exception, with extra data.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Information(Exception exception, string messageTemplate, params object[] propertyValue);
        
        /// <summary>
        /// For logging general information.
        /// </summary>
        /// <param name="messageTemplate">Information For Logging</param>
        void Debug(string messageTemplate);

        /// <summary>
        /// For logging debug information, with extra data.
        /// </summary>
        /// <param name="messageTemplate">Main message</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Debug(string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging debug information about an exception.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information.</param>
        void Debug(Exception exception, string messageTemplate);

        /// <summary>
        /// For logging debug information about an exception, with extra data.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Debug(Exception exception, string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging a warning.
        /// </summary>
        /// <param name="messageTemplate">Information For Logging</param>
        void Warning(string messageTemplate);
        
        /// <summary>
        /// For logging a warning, with extra data.
        /// </summary>
        /// <param name="messageTemplate">Main message</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Warning(string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging a warning about an exception.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information.</param>
        void Warning(Exception exception, string messageTemplate);

        /// <summary>
        /// For logging a warning about an exception, with extra data.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Warning(Exception exception, string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging fatal errors.
        /// </summary>
        /// <param name="messageTemplate">error message</param>
        void Error(string messageTemplate);

        /// <summary>
        /// For logging fatal errors, with extra data to concatenate.
        /// </summary>
        /// <param name="messageTemplate">error message</param>
        /// <param name="propertyValue">data to concatenate</param>
        void Error(string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging fatal errors, with a message.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information</param>
        void Error(Exception exception, string messageTemplate);

        /// <summary>
        /// For logging fatal errors, with extra data to concatenate.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">error message</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Error(Exception exception, string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging fatal information.
        /// </summary>
        /// <param name="messageTemplate">Information For Logging</param>
        void Fatal(string messageTemplate);

        /// <summary>
        /// For logging fatal information, with extra data.
        /// </summary>
        /// <param name="messageTemplate">Main message</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Fatal(string messageTemplate, params object[] propertyValue);

        /// <summary>
        /// For logging fatal information about an exception.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information.</param>
        void Fatal(Exception exception, string messageTemplate);

        /// <summary>
        /// For logging fatal information about an exception, with extra data.
        /// </summary>
        /// <param name="exception">exception to be logged.</param>
        /// <param name="messageTemplate">extra information</param>
        /// <param name="propertyValue">data to concatenate.</param>
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValue);
    }
}
