using System;
using Okapi.Logs;
using Serilog;
using SeriLogLogger = Serilog.Core.Logger;

namespace Okapi.Support.File
{
    public class Logger : IOkapiLogger
    {
        private static SeriLogLogger logger;

        public Logger(string fileName)
        {
            logger = new LoggerConfiguration().WriteTo.File(fileName).CreateLogger();
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