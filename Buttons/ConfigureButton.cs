using Inventor;
using Inventor.InternalNames.Ribbon;
using ViewReference.UI;

namespace ViewReference.Buttons
{
    internal class ConfigureButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        { 
            ConfigUI config = new ConfigUI();
            config.ShowDialog();
        }
        
        protected override string GetRibbonName() => InventorRibbons.Drawing;

        protected override string GetRibbonTabName() => DrawingRibbonTabs.PlaceViews;

        protected override string GetRibbonPanelName() => "View Reference";

        protected override string GetButtonName() => "Configure";

        protected override string GetDescriptionText() => "Configure View Reference";

        protected override string GetToolTipText() => "Select Options for View Reference.";
        
        protected override string GetLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-light-32px.bmp";
        }

        protected override string GetDarkThemeLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-dark-32px.bmp";
        }

        protected override string GetSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-light-16px.bmp";
        }

        protected override string GetDarkThemeSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.edit-dark-16px.bmp";
        }

        protected override bool UseLargeIcon => false;

        internal override int SequenceNumber => 2;
    }
}