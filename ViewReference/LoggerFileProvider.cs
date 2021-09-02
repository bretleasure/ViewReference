using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ViewReference
{
    //This was created because Inventor 2022 includes Serilog.dll in it's bin folder and was causing issues
    //because the MicrosoftExtensions.Logging and Serilog libraries were not in the same folder

    public class LoggerFileProvider : ILoggerProvider
    {
        private string _logPath;
        public LoggerFileProvider(string logPath)
        {
            _logPath = logPath;

            if (!File.Exists(_logPath))
            {
                var dirPath = Path.GetDirectoryName(_logPath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                File.Create(_logPath)
                    .Close();
            }
        }

        public ILogger CreateLogger(string categoryName) => new LoggerFile(this, categoryName);

        public void Dispose() { }

        internal void WriteMessage(string message) => File.AppendAllText(_logPath, message);
    }

    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
        {
            builder.Services.AddSingleton<ILoggerProvider>(new LoggerFileProvider(filePath));

            return builder;
        }
    }
}
