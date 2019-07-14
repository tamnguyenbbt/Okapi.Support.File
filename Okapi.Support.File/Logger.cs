using System;
using Okapi.Common;
using Okapi.Logs;
using Serilog;
using SeriLogLogger = Serilog.Core.Logger;

namespace Okapi.Support.File
{
    public class Logger : IOkapiLogger
    {
        private static SeriLogLogger logger;

        public Logger()
        {
            string logFilePath = Session.Instance.LogPath;
            new LoggerConfiguration().WriteTo.File(logFilePath).CreateLogger();
        }

        public void Error(string messageTemplate)
        {
            logger.Error(messageTemplate);
        }

        public void Error(string messageTemplate, Exception exception)
        {
            logger.Error(exception, messageTemplate);
        }

        public void Info(string messageTemplate)
        {
            logger.Information(messageTemplate);
        }
    }
}