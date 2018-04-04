using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using CAP.Utilities;

namespace CAP.Apps.ViewReference
{
    public abstract class ViewRefTools
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

        public static void AddReferencesToView(DrawingView oView, string LabelStyle)
        {
            InvView CurrentRefs = new InvView();
            CurrentRefs = ViewRefTools.GetSavedAttributesFromView(oView);

            //Step 1 - Remove Current References if they Exist
            ViewRefTools.ResetView(oView, CurrentRefs);

            //Step 2 - Create New References
            ViewRefTools.CreateViewReferences(oView, LabelStyle);

        }

        public static void ResetView(DrawingView oView, InvView iView)
        {
            if (iView != null)
            {
                oView.Name = iView.ViewName;
                RemoveLabelReferences(oView, iView);
                ClearViewAttributes(oView);
            }
            else
            {
                //References were never added, or they were added as the original app
                //Check for Original Referencesl
                //TODO: add check for original references in vb .dll file
            }
        }

        static void CreateViewReferences(DrawingView oView, string LabelStyle)
        {
            InvView iView = new InvView();

            //Get View Properties
            GetViewProperties(oView, out iView.ViewName, out iView.ViewSheetNumber, out iView.ViewSheetName, out iView.ParentSheetNumber, out iView.ParentSheetName);

            //View Callout
            oView.Name = CreateViewCallout(iView);

            //View Label
            iView.LabelText = oView.Label.FormattedText;
            oView.Label.FormattedText = CreateViewLabel(iView);


            //Save View Attributes
            SaveAttributesToView(oView, iView);
        }

        static string CreateViewCallout(InvView iView)
        {
            string StartString = AddinGlobal.vRefSettings.CalloutStyle;
            
            return ReplaceAttributesWithValues(StartString, iView);
        }

        static string CreateViewLabel(InvView iView)
        {
            //Get portion of View label that contains the View Name
            string ViewCalloutText = GetViewCalloutTextFromLabelText(iView.LabelText);

            return ReplaceAttributesWithValues(ViewCalloutText, iView);
        }

        /// <summary>
        /// This takes the Attribute string declared by user and replaces the attributes with the values and returns as a string.
        /// </summary>
        /// <param name="StartString"></param>
        /// <returns></returns>
        static string ReplaceAttributesWithValues(string StartString, InvView iView)
        {
            StartString.Replace("<VIEW>", iView.ViewName);
            StartString.Replace("<VIEW SHEET #>", iView.ViewSheetNumber);
            StartString.Replace("<VIEW SHEET NAME>", iView.ViewSheetName);
            StartString.Replace("<PARENT SHEET #>", iView.ParentSheetNumber);
            StartString.Replace("<PARENT SHEET NAME>", iView.ParentSheetName);

            return StartString;
        }

        static void GetViewProperties(DrawingView oView, out string ViewName, out string ViewSheetNumber, out string ViewSheetName, out string ParentSheetNumber, out string ParentSheetName)
        {
            ViewName = oView.Name;
            string Inv_SheetName = oView.Parent.Name;

            ViewSheetNumber = GetSheetNumber(Inv_SheetName);
            ViewSheetName = GetSheetName(Inv_SheetName);

            string Inv_ParentSheetName = oView.ParentView.Parent.Name;

            ParentSheetNumber = GetSheetNumber(Inv_ParentSheetName);
            ParentSheetName = GetSheetName(Inv_ParentSheetName);

        }

        static string GetSheetNumber(string Inventor_SheetName)
        {
            return Inventor_SheetName.Substring(Inventor_SheetName.IndexOf(":") + 1);
        }

        static string GetSheetName(string Inventor_SheetName)
        {
            return Inventor_SheetName.Substring(0, Inventor_SheetName.IndexOf(":") - 1);
        }

        private static void ClearViewAttributes(DrawingView oView)
        {
            oView.AttributeSets["ViewReference"].Delete();
        }

        /// <summary>
        /// This returns only the part of the View Label text that has the View Name in it
        /// </summary>
        /// <param name="LabelText"></param>
        /// <returns></returns>
        static string GetViewCalloutTextFromLabelText(string LabelText)
        {
            int ViewLength = ("<DrawingViewName/>").Length;

            return LabelText.Substring(LabelText.IndexOf("<DrawingViewName/>"), LabelText.LastIndexOf("<DrawingViewName/>") - LabelText.IndexOf("<DrawingViewName/>") + ViewLength);
        }

        static void RemoveLabelReferences(DrawingView oView, InvView iView)
        {
            string ViewLabel = oView.Label.FormattedText;

            if (iView.LabelText != "")
            {
                oView.Label.FormattedText = iView.LabelText;
            }
            else
            {
                //TODO: Add Old Way to Remove References
                //Label Text attribute doesnt exist, replace with default text from Standards Library
                string DefaultDetailText;
                string DefaultSectionText;
                string DefaultAuxText;

                string result = "";
                
                switch (oView.ViewType)
                {
                    case DrawingViewTypeEnum.kDetailDrawingViewType:
                        //result = ViewLabel.Replace(Label)
                        break;
                    case DrawingViewTypeEnum.kSectionDrawingViewType:

                        break;
                    case DrawingViewTypeEnum.kAuxiliaryDrawingViewType:

                        break;
                }

            }
        }

        static void Get_DefaultViewText(out string DetailText, out string SectionText, out string AuxText)
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
            //TODO: Need to add part that grabs only the portion that I need

        }
    }
}
