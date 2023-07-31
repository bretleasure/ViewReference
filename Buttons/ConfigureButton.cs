using Inventor;

namespace ViewReference.Buttons
{
    public class ConfigureButton : InventorButton
    {
        public override void Execute(NameValueMap context) => AddinGlobal.ShowConfigForm();

        public override string GetButtonName() => "Configure";

        public override string GetDescriptionText() => "Configure View Reference";

        public override string GetToolTipText() => "Select Options for View Reference.";

        public override string GetIconResourceName() => "Icons.gear.ico";
    }
}