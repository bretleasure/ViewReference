using System;
using System.Drawing;
using System.Reflection;
using Inventor;
using System.Windows.Forms;
using IPictureDisp = Inventor.IPictureDisp;

namespace ViewReference.Buttons
{
    public abstract class InventorButton
    {
        internal InventorButton()
        {
            Definition = AddinGlobal.InventorApp.CommandManager.ControlDefinitions.AddButtonDefinition(GetButtonName(), GetInternalName(),
                CommandType, null, GetDescriptionText(), GetToolTipText(), SmallIcon, LargeIcon);
            Definition.Enabled = true;
            Definition.OnExecute += Execute;
        }

        public abstract void Execute(NameValueMap context);
        public abstract string GetButtonName();
        public virtual string GetInternalName() => Guid.NewGuid().ToString();
        public abstract string GetDescriptionText();
        public abstract string GetToolTipText();
        public abstract string GetLargeIconResourceName();
        public abstract string GetDarkThemeLargeIconResourceName();
        public abstract string GetSmallIconResourceName();
        public abstract string GetDarkThemeSmallIconResourceName();
        public virtual CommandTypesEnum CommandType => CommandTypesEnum.kEditMaskCmdType;

        public ButtonDefinition Definition { get; }
        public bool Enabled
        {
            get => Definition.Enabled;
            set => Definition.Enabled = value;
        }

        private IPictureDisp LightThemeLargeIcon => CreateIcon(GetLargeIconResourceName());
        private IPictureDisp DarkThemeLargeIcon => CreateIcon(GetDarkThemeLargeIconResourceName());
        private IPictureDisp LightThemeSmallIcon => CreateIcon(GetSmallIconResourceName());
        private IPictureDisp DarkThemeSmallIcon => CreateIcon(GetDarkThemeSmallIconResourceName());
        
        private IPictureDisp LargeIcon
        {
            get
            {
                if (AddinGlobal.ActiveTheme.Name == "LightTheme")
                {
                    return LightThemeLargeIcon;
                }
                else
                {
                    return DarkThemeLargeIcon;
                }
            }
        }
        
        private IPictureDisp SmallIcon
        {
            get
            {
                if (AddinGlobal.ActiveTheme.Name == "LightTheme")
                {
                    return LightThemeSmallIcon;
                }
                else
                {
                    return DarkThemeSmallIcon;
                }
            }
        }

        private IPictureDisp CreateIcon(string resourceName)
        {
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            
            if (resourceStream == null)
                throw new Exception($"Resource {resourceName} not found in assembly {Assembly.GetExecutingAssembly().FullName}");
            
            var bitmap = new Bitmap(resourceStream);

            return ImageConverter.BitmapToPicture(bitmap);
        }
    }
    
    internal class ImageConverter : AxHost
    {
        public ImageConverter() : base(string.Empty) { }

        public static IPictureDisp BitmapToPicture(Bitmap bitmap)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(bitmap);
        }
    }
}
