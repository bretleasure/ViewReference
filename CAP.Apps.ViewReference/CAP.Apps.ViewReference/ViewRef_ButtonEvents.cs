using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using CAP.Utilities;

namespace CAP.Apps.ViewReference
{
    public class ViewRef_ButtonEvents
    {
        static Inventor.Application InvApp = AddinGlobal.InventorApp;

        static DrawingDocument dwgDoc;

        static ViewReference vRef = AddinGlobal.vRefSettings;

        public static void CreateUpdate_References()
        {
            if (AddinGlobal.vRefSettings == null)
            {
                DialogResult oDRes = MessageBox.Show("You have not configured View Reference.  Configure now?", "Configure View Reference", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                if (oDRes == DialogResult.Yes)
                    ShowConfigForm();

                return;
            }

            dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

            foreach (Sheet oSheet in dwgDoc.Sheets)
            {
                foreach (DrawingView oView in oSheet.DrawingViews)
                {
                    switch (oView.ViewType)
                    {
                        case DrawingViewTypeEnum.kDetailDrawingViewType:
                            if (vRef.DetailView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.DetailLabelStyle);
                            }
                            break;
                        case DrawingViewTypeEnum.kSectionDrawingViewType:
                            if (vRef.SectionView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.SectionLabelStyle);
                            }
                            break;
                        case DrawingViewTypeEnum.kAuxiliaryDrawingViewType:
                            if (vRef.AuxView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.AuxLabelStyle);
                            }
                            break;
                        case DrawingViewTypeEnum.kProjectedDrawingViewType:
                            if (vRef.ProjectedView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.ProjectedLabelStyle);
                            }
                            break;
                        case DrawingViewTypeEnum.kAssociativeDraftDrawingViewType:
                            if (vRef.AssociativeDraftView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.AssociativeDraftLabelStyle);
                            }
                            break;
                        case DrawingViewTypeEnum.kDraftDrawingViewType:
                            if (vRef.AssociativeDraftView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.DraftLabelStyle);
                            }
                            break;
                        case DrawingViewTypeEnum.kStandardDrawingViewType:
                            if (vRef.StandardView)
                            {
                                ViewRefTools.AddReferencesToView(oView, vRef.StandardLabelStyle);
                            }
                            break;
                        default:

                            break;
                    }

                } //View foreach

            } //Sheet foreach
            
        }        

        public static void Remove_References()
        {
            dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

            foreach (Sheet oSheet in dwgDoc.Sheets)
            {
                foreach (DrawingView oView in oSheet.DrawingViews)
                {
                    InvView SavedRefs = new InvView();
                    SavedRefs = ViewRefTools.GetSavedAttributesFromView(oView);

                    ViewRefTools.ResetView(oView, SavedRefs);
                    
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
