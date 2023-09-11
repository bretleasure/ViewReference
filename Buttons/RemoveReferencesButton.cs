using Inventor;

namespace ViewReference.Buttons
{
    internal class RemoveReferencesButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            if (AddinGlobal.InventorApp.ActiveDocument is DrawingDocument dwgDoc)
            {
                AddinGlobal.Automation.RemoveReferences(dwgDoc);
            }
        }

        public override string GetButtonName() => "Remove";

        public override string GetDescriptionText() => "Remove View References";

        public override string GetToolTipText() => "Remove View References in this document.";
        
        public override string GetLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-light-32px.bmp";
        }

        public override string GetDarkThemeLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-dark-32px.bmp";
        }

        public override string GetSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-light-16px.bmp";
        }

        public override string GetDarkThemeSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.delete-dark-16px.bmp";
        }
    }
}