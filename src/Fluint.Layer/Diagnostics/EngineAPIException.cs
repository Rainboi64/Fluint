using System;

namespace Fluint.Layer.Diagnostics
{
    public class EngineApiException : Exception
    {
        public EngineApiException()
        {
        }

        public EngineApiException(string api, string message) : base($"{api}: {message}")
        {
        }
    }
}