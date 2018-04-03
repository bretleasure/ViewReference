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

        public static string AppFolder = Tools.GetAppFolder("View Reference");

        public static string SettingsFile = Tools.GetHexString("View Reference Settings") + ".xml";

        public static string AppId = "5865579890990954428";

        public static ViewReference vRefSettings;

    }
}
