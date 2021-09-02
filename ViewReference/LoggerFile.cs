using System;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ViewReference
{
    //This was created because Inventor 2022 includes Serilog.dll in it's bin folder and was causing issues
    //because the MicrosoftExtensions.Logging and Serilog libraries were not in the same folder

    public class LoggerFile : ILogger
    {
        private readonly LoggerFileProvider _loggerProvider;
        private readonly string _categoryName;

        public LoggerFile(LoggerFileProvider loggerProvider, string categoryName)
        {
            _loggerProvider = loggerProvider;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"[{DateTime.Now}][{logLevel.ToString()}]: {formatter(state, exception)}");

            if (exception != null)
            {
                builder.AppendLine(exception.ToString());
            }

            _loggerProvider.WriteMessage(builder.ToString());
        }
    }
}
