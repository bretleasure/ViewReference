using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using ViewReference.Exceptions;

namespace ViewReference.Buttons
{
    internal class CreateReferencesButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            if (AddinGlobal.InventorApp.ActiveDocument is DrawingDocument dwgDoc)
            {
                var task = AddinGlobal.Automation.CreateReferences(AddinGlobal.Settings.ViewReferenceSettings, dwgDoc);

                if (task.Status == TaskStatus.Faulted)
                {
                    if (task.Exception?.InnerException is NotConfiguredException)
                    {
                        DialogResult response = MessageBox.Show("You have not configured View Reference. Configure now?", "Configure View Reference", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                        if (response == DialogResult.Yes)
                            AddinGlobal.ShowConfigForm();
                    }
                    else if (task.Exception?.InnerException is AddingViewReferencesException ex)
                    {
                        MessageBox.Show(ex.Message, "View Reference");
                    }
                }
            }
        }

        public override string GetButtonName() => "Create /\rUpdate";

        public override string GetDescriptionText() => "Create/Update View References";

        public override string GetToolTipText() => "Create/Update View References in this document.";

        public override string GetIconResourceName() => "Icons.ViewRef-Add.ico";
    }
}