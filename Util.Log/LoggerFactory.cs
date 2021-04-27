using System;

namespace Util.Log
{
    public interface LoggerFactory
    {
        void Info(string message);
        void Debug(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception ex, string message);
        void Info<T>(T logEntity);
        void Debug<T>(T logEntity);
        void Warn<T>(T logEntity);
        void Error<T>(T logEntity);
    }
}
