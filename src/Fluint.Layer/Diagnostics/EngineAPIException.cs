using System;

namespace Fluint.Layer.Diagnostics
{
    public class EngineAPIException : Exception
    {
        public EngineAPIException() {}
        public EngineAPIException(string API, string Message) : base($"{API}: {Message}")
        {}
    }
}