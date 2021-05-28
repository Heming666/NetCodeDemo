using System;

namespace Util.Log
{
    public interface ILoggerFactory
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
        /// <summary>
        /// 设置文件存储路径
        /// </summary>
        /// <param name="directoryName">文件存储路径的文件夹的名称</param>
        void Setting(string directoryName);
    }
}
