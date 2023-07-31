using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using iAD.Utilities;
using Path = System.IO.Path;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ViewReference
{
    public static class AddinGlobal
    {
        public static Inventor.Application InventorApp { get; set; }

        public static string SettingsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");

        public static string AppId { get; } = "5865579890990954428";

        public static ViewReferenceSettings Settings { get; set; }

        public static ViewReferenceAutomation Automation { get; set; }

		public static string AttributeSetName { get; } = "ViewReference-v4";
        
        public static void ShowConfigForm()
        {
            ConfigUI config = new ConfigUI();
            config.ShowDialog();
        }
    }
}
