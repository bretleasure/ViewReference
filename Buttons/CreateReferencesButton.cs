using System;
using System.CodeDom;
using System.Linq;
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
                var task = AddinGlobal.Automation.CreateReferences(dwgDoc, AddinGlobal.Settings.ViewReferenceSettings);

                if (task.Status == TaskStatus.Faulted)
                {
                    var exceptions = task.Exception.InnerExceptions;
                    if (exceptions.Any(e => e is NotConfiguredException))
                    {
                        MessageBox.Show("You have not configured View Reference",
                            "View Reference", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (exceptions.Any(e => e is AddingViewReferencesException))
                    {
                        var viewNames = exceptions.Where(e => e is AddingViewReferencesException)
                            .Cast<AddingViewReferencesException>()
                            .Select(e => e.ViewName);
                        
                        var viewNamesString = string.Join(System.Environment.NewLine, viewNames);
                        var message = string.Join(System.Environment.NewLine, $"Adding View References Failed for views:{System.Environment.NewLine}", viewNamesString);
                        MessageBox.Show(message, "View Reference", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public override string GetButtonName() => "Create /\rUpdate";

        public override string GetDescriptionText() => "Create/Update View References";

        public override string GetToolTipText() => "Create/Update View References in this document.";
        
        public override string GetLargeIconResourceName()
        {
            throw new NotImplementedException();
        }

        public override string GetDarkThemeLargeIconResourceName()
        {
            throw new NotImplementedException();
        }

        public override string GetSmallIconResourceName()
        {
            throw new NotImplementedException();
        }

        public override string GetDarkThemeSmallIconResourceName()
        {
            throw new NotImplementedException();
        }
    }
}