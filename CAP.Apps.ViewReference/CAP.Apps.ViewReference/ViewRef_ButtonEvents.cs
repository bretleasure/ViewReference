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
            dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

            foreach (DrawingView oView in dwgDoc.Views)
            {
                switch (oView.ViewType)
                {
                    case DrawingViewTypeEnum.kDetailDrawingViewType:
                        if (vRef.DetailView)
                        {
                            AddReferences(oView, vRef.DetailLabelStyle);
                        }
                        break;
                    case DrawingViewTypeEnum.kSectionDrawingViewType:
                        if (vRef.SectionView)
                        {
                            AddReferences(oView, vRef.SectionLabelStyle);
                        }
                        break;
                    case DrawingViewTypeEnum.kAuxiliaryDrawingViewType:
                        if (vRef.AuxView)
                        {
                            AddReferences(oView, vRef.AuxLabelStyle);
                        }
                        break;
                    case DrawingViewTypeEnum.kProjectedDrawingViewType:
                        if (vRef.ProjectedView)
                        {
                            AddReferences(oView, vRef.ProjectedLabelStyle);
                        }
                        break;
                    case DrawingViewTypeEnum.kAssociativeDraftDrawingViewType:
                        if (vRef.AssociativeDraftView)
                        {
                            AddReferences(oView, vRef.AssociativeDraftLabelStyle);
                        }
                        break;
                    case DrawingViewTypeEnum.kDraftDrawingViewType:
                        if (vRef.AssociativeDraftView)
                        {
                            AddReferences(oView, vRef.DraftLabelStyle);
                        }
                        break;
                    case DrawingViewTypeEnum.kStandardDrawingViewType:
                        if (vRef.StandardView)
                        {
                            AddReferences(oView, vRef.StandardLabelStyle);
                        }
                        break;
                    default:


                        break;
                }


            }

        }

        static void AddReferences(DrawingView oView, string LabelStyle)
        {
            InvView CurrentRefs = new InvView();
            CurrentRefs = ViewRefTools.GetSavedAttributesFromView(oView);

            //Step 1 - Remove Current References if they Exist
            

            InvView NewRefs = new InvView();
            NewRefs.CalloutStyle = vRef.CalloutStyle;
            NewRefs.ViewLabelStyle = LabelStyle;





            //Save Attributes to View
            ViewRefTools.SaveAttributesToView(oView, NewRefs);
        }

        public static void Remove_References()
        {


        }

        public static void ShowConfigForm()
        {

            
        }

        
    }
}
