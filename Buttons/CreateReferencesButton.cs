using System;
using System.CodeDom;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using Inventor.InternalNames.Ribbon;
using ViewReference.Exceptions;

namespace ViewReference.Buttons
{
    internal class CreateReferencesButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            if (inventor.ActiveDocument is DrawingDocument dwgDoc)
            {
                var task = AddinServer.AppAutomation.CreateReferences(dwgDoc, AddinServer.Settings.ViewReferenceSettings);

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
        
        protected override string GetRibbonName() => InventorRibbons.Drawing;

        protected override string GetRibbonTabName() => DrawingRibbonTabs.PlaceViews;

        protected override string GetRibbonPanelName() => "View Reference";

        protected override string GetButtonName() => "Create /\rUpdate";

        protected override string GetDescriptionText() => "Create/Update View References";

        protected override string GetToolTipText() => "Create/Update View References in this document.";
        
        protected override string GetLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.add-light-32px.bmp";
        }

        protected override string GetDarkThemeLargeIconResourceName()
        {
            return "ViewReference.Buttons.Icons.add-dark-32px.bmp";
        }

        protected override string GetSmallIconResourceName()
        {   
            return "ViewReference.Buttons.Icons.add-light-16px.bmp";
        }

        protected override string GetDarkThemeSmallIconResourceName()
        {
            return "ViewReference.Buttons.Icons.add-dark-16px.bmp";
        }
        
        protected override bool UseLargeIcon => true;

        internal override int SequenceNumber => 0;
    }
}