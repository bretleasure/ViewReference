using Inventor;
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

        public override string GetIconResourceName() => "Icons.gear.ico";
    }
}