using System;
using System.Collections.Generic;
using System.Linq;
using Inventor;
using Newtonsoft.Json;
using File = System.IO.File;

namespace ViewReference
{
    public static class ViewReferenceTools
    {
        public static void GetSavedSettings()
        {
            if (System.IO.File.Exists(AddinGlobal.SettingsFilePath))
            {
                var settingsJson = File.ReadAllText(AddinGlobal.SettingsFilePath);
                AddinGlobal.Settings = JsonConvert.DeserializeObject<ViewReferenceSettings>(settingsJson);
            }
			else
			{
                AddinGlobal.Settings = ViewReferenceSettings.Default;
                SaveSettings();
			}
        }

        public static void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(AddinGlobal.Settings);
            File.WriteAllText(AddinGlobal.SettingsFilePath, json);
        }   
        
        public static void CreateUpdateEventListener()
        {
            if (AddinGlobal.Settings != null)
            {
                if (AddinGlobal.Settings.UpdateBeforeSave)
                {
                    AddinGlobal.InventorApp.ApplicationEvents.OnSaveDocument += ApplicationEvents_OnSaveDocument;
                }
                else
                {
                    AddinGlobal.InventorApp.ApplicationEvents.OnSaveDocument -= ApplicationEvents_OnSaveDocument;
                }
            }
            
        }

        private static void ApplicationEvents_OnSaveDocument(_Document documentObject, EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            if (beforeOrAfter == EventTimingEnum.kBefore)
            {
                if (documentObject is DrawingDocument dwgDoc)
                {
                    if (!dwgDoc.ViewReferencesExistInDocument())
                    {
                        AddinGlobal.Automation.CreateReferences(AddinGlobal.Settings);
                    }
                }
            }

            handlingCode = HandlingCodeEnum.kEventHandled;
        }

        /// <summary>
        /// This returns only the part of the View Label text that has the View Name in it
        /// </summary>
        /// <param name="labelText"></param>
        /// <returns></returns>
        public static string GetViewCalloutTextFromLabelText(this string labelText)
        {
            //DETAIL <DrawingViewName/> 

            int ViewLength = ("<DrawingViewName/>").Length;

            return labelText.Substring(labelText.IndexOf("<DrawingViewName/>"), labelText.LastIndexOf("<DrawingViewName/>") - labelText.IndexOf("<DrawingViewName/>") + ViewLength);
            //return LabelText.Substring(LabelText.IndexOf("<DrawingViewName/>"), LabelText.LastIndexOf("<DrawingViewName/>") + ViewLength);
        }

        static void Get_DefaultViewText(out string detailText, out string sectionText, out string auxText)
        {
            DrawingDocument oDoc = (DrawingDocument)AddinGlobal.InventorApp.ActiveDocument;

            DrawingStylesManager oStylesMan = oDoc.StylesManager;

            DrawingStandardStyle oStandard = oStylesMan.ActiveStandardStyle;

            string sPrefix = "";
            bool bVisible;
            bool bConstrainToBorder;
            bool bPlaceBelowView;

            oStandard.GetViewLabelDefaults(DrawingViewTypeEnum.kDetailDrawingViewType, out sPrefix, out bVisible, out detailText, out bConstrainToBorder, out bPlaceBelowView);
            oStandard.GetViewLabelDefaults(DrawingViewTypeEnum.kSectionDrawingViewType, out sPrefix, out bVisible, out sectionText, out bConstrainToBorder, out bPlaceBelowView);
            oStandard.GetViewLabelDefaults(DrawingViewTypeEnum.kAuxiliaryDrawingViewType, out sPrefix, out bVisible, out auxText, out bConstrainToBorder, out bPlaceBelowView);

            int viewLength = ("<DrawingViewName/>").Length;

            //DetailText = 
            //TODO: Need to add part that grabs only the portion that I need

        }
    }
}
