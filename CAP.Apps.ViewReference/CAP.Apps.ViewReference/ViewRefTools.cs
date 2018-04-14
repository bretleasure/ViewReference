using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using CAP.Utilities;
using RemoveOldViewReferences.RemoveOldViewReferences;

namespace CAP.Apps.ViewReference
{
    public abstract class ViewRefTools
    {
        public static InvView GetSavedAttributesFromView(DrawingView oView)
        {
            InvView iView = new InvView();

            if (oView.AttributeSets.NameIsUsed["ViewReference-v4"])
            {
                //Attributes Exist

                AttributeSet oAttribSet = oView.AttributeSets["ViewReference-v4"];

                iView.ViewName = oAttribSet["ViewName"].Value.ToString();
                iView.ViewSheetName = oAttribSet["ViewSheetName"].Value.ToString();
                iView.ViewSheetNumber = oAttribSet["ViewSheetNumber"].Value.ToString();
                iView.ParentSheetName = oAttribSet["ParentSheetName"].Value.ToString();
                iView.ParentSheetNumber = oAttribSet["ParentSheetName"].Value.ToString();

                iView.CalloutStyle = oAttribSet["CalloutStyle"].Value.ToString();
                iView.ViewLabelStyle = oAttribSet["ViewLabelStyle"].Value.ToString();

                iView.LabelText = oAttribSet["LabelText"].Value.ToString();

            }
            else
            {
                iView = null;
            }

            return iView;

        }

        public static void SaveAttributesToView(DrawingView dwgView, InvView iView)
        {
            if (!dwgView.AttributeSets.NameIsUsed["ViewReference-v4"])
            {
                dwgView.AttributeSets.Add("ViewReference-v4", true);
            }

            AttributeSet oAttribSet = dwgView.AttributeSets["ViewReference-v4"];

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
            RecordLog("View Name = " + oView.Name);
            RecordLog("View Type = " + oView.ViewType.ToString());
            RecordLog("View Label = " + oView.Label.FormattedText);
            RecordLog("View Loc = " + oView.Parent.Name);

            string ViewName = oView.Name;
            try
            {
                if (oView.ParentView != null)
                {
                    InvView CurrentRefs = new InvView();
                    CurrentRefs = GetSavedAttributesFromView(oView);

                    //Step 1 - Remove Current References if they Exist
                    ResetView(oView, CurrentRefs);

                    //Step 2 - Create New References
                    CreateViewReferences(oView, LabelStyle);

                    RecordLog("Adding Reference Successful");
                }
                
            }
            catch (Exception e)
            {
                RecordLog(e.ToString());

                RecordLog("Adding Reference Failed");
            }

            RecordLog("-----------------------------------------------------------------------------");

        }

        public static void ResetView(DrawingView oView, InvView iView)
        {
            if (iView != null)
            {
                oView.Name = iView.ViewName;
                oView.Label.FormattedText = iView.LabelText;

                //RemoveLabelReferences(oView, iView);
                ClearViewAttributes(oView);
            }
            else
            {
                if (OldReferencesExist(oView))
                {
                    //Remove Old References
                    ViewRef_Remove OldVR = new ViewRef_Remove();
                    OldVR.Remove_ViewRefs(AddinGlobal.InventorApp);

                    //Remove Old ViewReference Attribute Set
                    if (oView.AttributeSets.NameIsUsed["ViewReference"])
                        oView.AttributeSets["ViewReference"].Delete();
                }

                //References were never added, or they were added as the original app

            }
        }

        static void RecordLog(string text)
        {
            string filepath = @"C:\Users\Public\Documents\ViewReferenceLog";

            LogFile(filepath, text);
        }

        static void LogFile(string filePath, string text)
        {
            using (StreamWriter file = new StreamWriter(filePath + ".txt", true))
            {
                file.WriteLine(text);
            }
        }

        static bool OldReferencesExist(DrawingView oView)
        {
            if (oView.AttributeSets.NameIsUsed["ViewReference"])
                return true;
            else
            {
                if (!oView.Label.FormattedText.Contains("<DrawingViewName/>"))
                {
                    //Original References Exist
                    return true;
                }
                else
                {
                    //No References Exist
                    return false;
                }
                    
            }
        }

        static void CreateViewReferences(DrawingView oView, string LabelStyle)
        {
            InvView iView = new InvView();

            iView.CalloutStyle = AddinGlobal.vRefSettings.CalloutStyle;
            iView.ViewLabelStyle = LabelStyle;

            //Get View Properties
            GetViewProperties(oView, out iView.ViewName, out iView.ViewSheetNumber, out iView.ViewSheetName, out iView.ParentSheetNumber, out iView.ParentSheetName);

            //View Callout
            oView.Name = CreateViewCallout(iView);

            //View Label
            iView.LabelText = oView.Label.FormattedText;
            oView.Label.FormattedText = CreateViewLabel(iView, LabelStyle, oView.Label.FormattedText);


            //Save View Attributes
            SaveAttributesToView(oView, iView);
        }

        static string CreateViewCallout(InvView iView)
        {
            string StartString = iView.CalloutStyle;
            
            return ReplaceAttributesWithValues(StartString, iView);
        }

        static string CreateViewLabel(InvView iView, string LabelStyle, string CurrentLabelText)
        {
            //Get portion of View label that contains the View Name
            string ViewCalloutText = GetViewCalloutTextFromLabelText(CurrentLabelText);

            string NewViewCalloutText = ReplaceAttributesWithValues(LabelStyle, iView);

            return CurrentLabelText.Replace(ViewCalloutText, NewViewCalloutText);
        }

        /// <summary>
        /// This takes the Attribute string declared by user and replaces the attributes with the values and returns as a string.
        /// </summary>
        /// <param name="StartString"></param>
        /// <returns></returns>
        static string ReplaceAttributesWithValues(string StartString, InvView iView)
        {
            StartString = StartString.Replace("<VIEW>", iView.ViewName);
            StartString = StartString.Replace("<VIEW SHEET #>", iView.ViewSheetNumber);
            StartString = StartString.Replace("<VIEW SHEET NAME>", iView.ViewSheetName);
            StartString = StartString.Replace("<PARENT SHEET #>", iView.ParentSheetNumber);
            StartString = StartString.Replace("<PARENT SHEET NAME>", iView.ParentSheetName);

            StartString = StartString.Replace("<DELIM>", "<Delimiter/>");
            StartString = StartString.Replace("<SCALE>", "<DrawingViewScale/>");

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
            return Inventor_SheetName.Substring(0, Inventor_SheetName.IndexOf(":"));
        }

        /// <summary>
        /// Clears All attributes from View by Deleting ViewReference Attribute Set
        /// </summary>
        /// <param name="oView"></param>
        static void ClearViewAttributes(DrawingView oView)
        {
            oView.AttributeSets["ViewReference-v4"].Delete();
        }

        /// <summary>
        /// This returns only the part of the View Label text that has the View Name in it
        /// </summary>
        /// <param name="LabelText"></param>
        /// <returns></returns>
        static string GetViewCalloutTextFromLabelText(string LabelText)
        {
            //DETAIL <DrawingViewName/> 

            int ViewLength = ("<DrawingViewName/>").Length;

            return LabelText.Substring(LabelText.IndexOf("<DrawingViewName/>"), LabelText.LastIndexOf("<DrawingViewName/>") - LabelText.IndexOf("<DrawingViewName/>") + ViewLength);
            //return LabelText.Substring(LabelText.IndexOf("<DrawingViewName/>"), LabelText.LastIndexOf("<DrawingViewName/>") + ViewLength);
        }

        static void RemoveLabelReferences(DrawingView oView, InvView iView)
        {

            //string ViewLabel = oView.Label.FormattedText;

            //if (iView.LabelText != "")
            //{
            //    oView.Label.FormattedText = iView.LabelText;
            //}
            //else
            //{
            //    //TODO: Add Old Way to Remove References
            //    //Label Text attribute doesnt exist, replace with default text from Standards Library
            //    string DefaultDetailText;
            //    string DefaultSectionText;
            //    string DefaultAuxText;

            //    string result = "";
                
            //    switch (oView.ViewType)
            //    {
            //        case DrawingViewTypeEnum.kDetailDrawingViewType:
            //            //result = ViewLabel.Replace(Label)
            //            break;
            //        case DrawingViewTypeEnum.kSectionDrawingViewType:

            //            break;
            //        case DrawingViewTypeEnum.kAuxiliaryDrawingViewType:

            //            break;
            //    }

            //}
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
