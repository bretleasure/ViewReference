using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using CAP.Utilities;

namespace CAP.Apps.ViewReference
{
    public class ViewRefTools
    {
        public static InvView GetSavedAttributesFromView(DrawingView oView)
        {
            InvView iView = new InvView();

            if (oView.AttributeSets.NameIsUsed["ViewReference"])
            {
                //Attributes Exist

                AttributeSet oAttribSet = oView.AttributeSets["ViewReference"];

                iView.ViewName = oAttribSet["ViewName"].Value.ToString();
                iView.ViewSheetName = oAttribSet["ViewSheetName"].Value.ToString();
                iView.ViewSheetNumber = oAttribSet["ViewSheetNumber"].Value.ToString();
                iView.ParentSheetName = oAttribSet["ParentSheetName"].Value.ToString();
                iView.ParentSheetNumber = oAttribSet["ParentSheetName"].Value.ToString();

                iView.CalloutStyle = oAttribSet["CalloutStyle"].Value.ToString();
                iView.ViewLabelStyle = oAttribSet["ViewLabelStyle"].Value.ToString();

                iView.LabelText = oAttribSet["LabelText"].Value.ToString();


                //iView.DetailStyle = oAttribSet["DetailStyle"].Value.ToString();
                //iView.SectionStyle = oAttribSet["SectionStyle"].Value.ToString();
                //iView.AuxStyle = oAttribSet["AuxStyle"].Value.ToString();
                //iView.BaseStyle = oAttribSet["BaseStyle"].Value.ToString();
                //iView.ProjectedStyle = oAttribSet["ProjectedStyle"].Value.ToString();

                //TODO: ADD WAY TO GET OLD STYLE ATTRIBUTES FROM VIEW
            }
            else
            {
                iView = null;
            }

            return iView;

        }

        public static void SaveAttributesToView(DrawingView dwgView, InvView iView)
        {
            if (!dwgView.AttributeSets.NameIsUsed["ViewReference"])
            {
                dwgView.AttributeSets.Add("ViewReference", true);
            }

            AttributeSet oAttribSet = dwgView.AttributeSets["ViewReference"];

            AssignAttributeValue(oAttribSet, "ViewName", iView.ViewName);
            AssignAttributeValue(oAttribSet, "ViewSheetName", iView.ViewSheetName);
            AssignAttributeValue(oAttribSet, "ViewSheetNumber", iView.ViewSheetNumber);
            AssignAttributeValue(oAttribSet, "ParentSheetName", iView.ParentSheetName);
            AssignAttributeValue(oAttribSet, "ParentSheetNumber", iView.ParentSheetNumber);
            AssignAttributeValue(oAttribSet, "CalloutStyle", iView.CalloutStyle);
            AssignAttributeValue(oAttribSet, "ViewLabelStyle", iView.ViewLabelStyle);
            AssignAttributeValue(oAttribSet, "LabelText", iView.LabelText);

        }

        private static void AssignAttributeValue(AttributeSet AttribSet, string AttributeName, string AttributeValue)
        {
            if (AttribSet.NameIsUsed[AttributeName])
            {
                //Attribute Exists, Assign Value
                AttribSet[AttributeName].Value = AttributeValue;
            }
            else
            {
                //Attribute Doesn't Exist, Create then Assign Value
                Inventor.Attribute newAttribute = AttribSet.Add(AttributeName, ValueTypeEnum.kStringType, AttributeValue);
                newAttribute.Value = AttributeValue;
            }
        }

        public static void Get_SavedSettings()
        {
            try
            {
                ViewReference vRef = new ViewReference();
                vRef = (ViewReference)XMLTools.Get_ObjectFromXML(AddinGlobal.AppFolder + AddinGlobal.SettingsFile, vRef);

                AddinGlobal.vRefSettings = vRef;
            }
            catch { }
            
        }

        public static void RemoveLabelReferences(DrawingView oView, InvView iView)
        {
            string ViewLabel = oView.Label.FormattedText;

            if (iView.LabelText != "")
            {
                oView.Label.FormattedText = iView.LabelText;
            }
            else
            {
                //Label Text attribute doesnt exist, replace with default text from Standards Library
                string DefaultDetailText;
                string DefaultSectionText;
                string DefaultAuxText;


            }
        }

        private static void Get_DefaultViewText(out string DetailText, out string SectionText, out string AuxText)
        {
            DrawingDocument oDoc = (DrawingDocument)AddinGlobal.InventorApp.ActiveDocument;

            DrawingStylesManager oStylesMan = oDoc.StylesManager;

            DrawingStandardStyle oStandard = oStylesMan.ActiveStandardStyle;

            string sPrefix = "";
            bool bVisible;
            bool bConstrainToBorder;
            bool bPlaceBelowView;

            oStandard.GetViewLabelDefaults(DrawingViewTypeEnum.kDetailDrawingViewType, out sPrefix, out bVisible, out DetailText, out bConstrainToBorder, out bPlaceBelowView);
            oStandard.GetViewLabelDefaults(DrawingViewTypeEnum.kSectionDrawingViewType, out sPrefix, out bVisible, out SectionText, out bConstrainToBorder, out bPlaceBelowView);
            oStandard.GetViewLabelDefaults(DrawingViewTypeEnum.kAuxiliaryDrawingViewType, out sPrefix, out bVisible, out AuxText, out bConstrainToBorder, out bPlaceBelowView);

            int ViewLength = ("<DrawingViewName/>").Length;

            //DetailText = 

        }
    }
}
