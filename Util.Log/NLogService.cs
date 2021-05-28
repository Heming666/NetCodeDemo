using NLog;
using System;
using Util.Extension;

namespace Util.Log
{
    public class NLogService : ILoggerFactory
    {
        public static  NLog.Logger _logger { get; private set; }
        public NLogService(string configPath)
        {
            _logger = LogManager.LoadConfiguration(configPath).GetCurrentClassLogger();
        }
        /// <summary>
        /// 自定义文件存放位置
        /// </summary>
        /// <param name="directoryName"></param>
        public void Setting(string directoryName)
        {
            if (!directoryName.IsEmpty())
            {
                LogManager.Configuration.Variables["cuspath"] = directoryName + "/";
            }
        }
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug<T>(T logEntity)
        {
            _logger.Debug(logEntity);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex, string message)
        {
            _logger.Error(ex,message);
        }

        public void Error<T>(T logEntity)
        {
            _logger.Error(logEntity);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info<T>(T logEntity)
        {
            _logger.Info(logEntity);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn<T>(T logEntity)
        {
            _logger.Warn(logEntity);
        }
    }
}
