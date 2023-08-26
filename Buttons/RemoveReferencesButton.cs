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
            throw new System.NotImplementedException();
        }

        public override string GetDarkThemeLargeIconResourceName()
        {
            throw new System.NotImplementedException();
        }

        public override string GetSmallIconResourceName()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDarkThemeSmallIconResourceName()
        {
            throw new System.NotImplementedException();
        }
    }
}