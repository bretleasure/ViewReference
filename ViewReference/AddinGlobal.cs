using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using iAD.Utilities;
using CAP.Utilities;
using Path = System.IO.Path;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ViewReference
{
    public class AddinGlobal
    {
        public static Inventor.Application InventorApp;

        public static string AppFolder = Tools.GetAppFolder("ViewReference");

        public static string SettingsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");

        public static string AppId = "5865579890990954428";

        public static ViewReference_Settings AppSettings;

        public static string LogFile = @"C:\Users\Public\Documents\Autodesk\View Reference Log";

        public static ILogger Logger;

        public static ILogger<T> GetLogger<T>()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(logging =>
                {
                    logging.AddFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\logs\Log.txt");
                })
                .BuildServiceProvider();

            return serviceProvider.GetService<ILogger<T>>();

        }
    }
}
