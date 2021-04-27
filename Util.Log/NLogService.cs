using System;

namespace Util.Log
{
    public class NLogService : LoggerFactory
    {
        private NLog.Logger _logger;
        public NLogService(string configPath)
        {
            _logger = NLog.LogManager.LoadConfiguration(configPath).GetCurrentClassLogger();
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
