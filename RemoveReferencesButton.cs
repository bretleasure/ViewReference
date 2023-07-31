using Inventor;

namespace ViewReference
{
    public class RemoveReferencesButton : InventorButton
    {
        public override void Execute(NameValueMap context) => AddinGlobal.Automation.RemoveReferences();

        public override string GetButtonName() => "Remove";

        public override string GetDescriptionText() => "Remove View References";

        public override string GetToolTipText() => "Remove View References in this document.";

        public override string GetIconResourceName() => "Resources.ViewRef-Remove.ico";
    }
}