﻿using Inventor;
using ViewReference.UI;

namespace ViewReference.Buttons
{
    internal class ConfigureButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        { 
            ConfigUI config = new ConfigUI();
            config.ShowDialog();
        }

        public override string GetButtonName() => "Configure";

        public override string GetDescriptionText() => "Configure View Reference";

        public override string GetToolTipText() => "Select Options for View Reference.";
        
        public override string GetLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-light-32px.bmp";
        }

        public override string GetDarkThemeLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-dark-32px.bmp";
        }

        public override string GetSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-light-16px.bmp";
        }

        public override string GetDarkThemeSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-dark-16px.bmp";
        }
    }
}