using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using iAD.Utilities;
using CAP.Utilities;
using Microsoft.Extensions.Logging;

namespace ViewReference
{
    public class ViewReference_ButtonEvents
    {
       
        static Inventor.Application InvApp = AddinGlobal.InventorApp;

        static DrawingDocument dwgDoc;

        static ViewReference_Settings oSettings;

        public static void CreateUpdate_References()
        {
            try
            {
                //Check Entitlement
                if (!Tools.CheckForValidUser(AddinGlobal.InventorApp, "ViewReference", AddinGlobal.AppId))
                {
                    AddinGlobal.Logger.LogInformation("User does not have valid entitlement");
                    return;
                }

                if (AddinGlobal.AppSettings == null)
                {
                    DialogResult oDRes = MessageBox.Show("You have not configured View Reference.  Configure now?", "Configure View Reference", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                    if (oDRes == DialogResult.Yes)
                        ShowConfigForm();

                    return;
                }

                dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

                oSettings = AddinGlobal.AppSettings;

                foreach (Sheet oSheet in dwgDoc.Sheets)
                {
                    foreach (DrawingView oView in oSheet.DrawingViews)
                    {
                        switch (oView.ViewType)
                        {
                            case DrawingViewTypeEnum.kDetailDrawingViewType:
                                if (oSettings.DetailView)
                                {
                                    ViewReference_Tools.AddReferencesToView(oView, oSettings.DetailLabelStyle);
                                }
                                break;
                            case DrawingViewTypeEnum.kSectionDrawingViewType:
                                if (oSettings.SectionView)
                                {
                                    ViewReference_Tools.AddReferencesToView(oView, oSettings.SectionLabelStyle);
                                }
                                break;
                            case DrawingViewTypeEnum.kAuxiliaryDrawingViewType:
                                if (oSettings.AuxView)
                                {
                                    ViewReference_Tools.AddReferencesToView(oView, oSettings.AuxLabelStyle);
                                }
                                break;
                            case DrawingViewTypeEnum.kProjectedDrawingViewType:
                                if (oSettings.ProjectedView)
                                {
                                    ViewReference_Tools.AddReferencesToView(oView, oSettings.ProjectedLabelStyle);
                                }
                                break;
                            default:

                                break;
                        }

                    } //View foreach

                } //Sheet foreach
            }
            catch (Exception ex)
            {
                AddinGlobal.Logger.LogError(ex, "An error occurred while adding View References");
                MessageBox.Show("An error occurred wile adding View References. Check log for details");
            }
        }

        public static void Remove_References()
        {
            //Check Entitlement
            if (!Tools.CheckForValidUser(AddinGlobal.InventorApp, "ViewReference", AddinGlobal.AppId))
            {
                AddinGlobal.Logger.LogInformation("User does not have valid entitlement");
                return;
            }

            dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

            foreach (Sheet oSheet in dwgDoc.Sheets)
            {
                foreach (DrawingView oView in oSheet.DrawingViews)
                {
                    InvView SavedRefs = new InvView();
                    SavedRefs = ViewReference_Tools.GetSavedAttributesFromView(oView);

                    ViewReference_Tools.ResetView(oView, SavedRefs);

                }
            }

        }

        public static void ShowConfigForm()
        {
            ConfigUI config = new ConfigUI();
            config.ShowDialog();
        }


    }
}
