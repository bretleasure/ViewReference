using Inventor;
using Inventor.InternalNames.Ribbon;

namespace ViewReference.Buttons
{
    internal class RemoveReferencesButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            if (inventor.ActiveDocument is DrawingDocument dwgDoc)
            {
                AddinServer.AppAutomation.RemoveReferences(dwgDoc);
            }
        }

        protected override string GetRibbonName() => InventorRibbons.Drawing;

        protected override string GetRibbonTabName() => DrawingRibbonTabs.PlaceViews;

        protected override string GetRibbonPanelName() => "View Reference";

        protected override string GetButtonName() => "Remove";

        protected override string GetDescriptionText() => "Remove View References";

        protected override string GetToolTipText() => "Remove View References in this document.";
        
        protected override string GetLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-light-32px.bmp";
        }

        protected override string GetDarkThemeLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-dark-32px.bmp";
        }

        protected override string GetSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-light-16px.bmp";
        }

        protected override string GetDarkThemeSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-dark-16px.bmp";
        }
        
        protected override bool UseLargeIcon => true;

        internal override int SequenceNumber => 1;
    }
}