using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using CAP.Utilities;

namespace CAP.Apps.ViewReference
{
    public class AddinGlobal
    {
        public static Inventor.Application InventorApp;

        public static string AppFolder = Tools.GetAppFolder("CAP.Apps.ViewReference");

        public static string SettingsFile = Tools.GetHexString("CAP.Apps.ViewReference Settings") + ".xml";

        public static string AppId = "5865579890990954428";

        public static ViewReference_Settings AppSettings;

        public static string LogFile = @"C:\Users\Public\Documents\Autodesk\View Reference Log";

    }
}
