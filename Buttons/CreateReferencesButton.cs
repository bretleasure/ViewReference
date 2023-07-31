using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using ViewReference.Exceptions;

namespace ViewReference.Buttons
{
    public class CreateReferencesButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            var task = AddinGlobal.Automation.CreateReferences(AddinGlobal.Settings);
            if (task.Status == TaskStatus.Faulted)
            {
                if (task.Exception is NotConfiguredException)
                {
                    DialogResult response = MessageBox.Show("You have not configured View Reference. Configure now?", "Configure View Reference", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                    if (response == DialogResult.Yes)
                        AddinGlobal.ShowConfigForm();
                }
                else if (task.Exception is AddingViewReferencesException ex)
                {
                    MessageBox.Show(ex.Message, "View Reference");
                }
            }
        }

        public override string GetButtonName() => "Create /\rUpdate";

        public override string GetDescriptionText() => "Create/Update View References";

        public override string GetToolTipText() => "Create/Update View References in this document.";

        public override string GetIconResourceName() => "Icons.ViewRef-Add.ico";
    }
}