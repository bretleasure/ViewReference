using System;
using System.Collections.Generic;
using System.Linq;
using Inventor;
using Newtonsoft.Json;
using File = System.IO.File;

namespace ViewReference
{
    internal static class ViewReferenceTools
    {
        internal static void GetSavedSettings()
        {
            if (System.IO.File.Exists(AddinServer.SettingsFilePath))
            {
                var settingsJson = File.ReadAllText(AddinServer.SettingsFilePath);
                AddinServer.Settings = JsonConvert.DeserializeObject<ViewReferenceAddinSettings>(settingsJson);
            }
			else
            {
                AddinServer.Settings = new ViewReferenceAddinSettings
                {
                    ViewReferenceSettings = ViewReferenceSettings.Default,
                    UpdateBeforeSave = false
                };
                SaveSettings();
			}
        }

        internal static void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(AddinServer.Settings);
            File.WriteAllText(AddinServer.SettingsFilePath, json);
        }   
        
        internal static void CreateUpdateEventListener()
        {
            if (AddinServer.Settings != null)
            {
                if (AddinServer.Settings.UpdateBeforeSave)
                {
                    AddinServer.InventorApp.ApplicationEvents.OnSaveDocument += ApplicationEvents_OnSaveDocument;
                }
                else
                {
                    AddinServer.InventorApp.ApplicationEvents.OnSaveDocument -= ApplicationEvents_OnSaveDocument;
                }
            }
        }

        private static void ApplicationEvents_OnSaveDocument(_Document documentObject, EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            if (beforeOrAfter == EventTimingEnum.kBefore)
            {
                if (documentObject is DrawingDocument dwgDoc)
                {
                    if (dwgDoc.ViewReferencesExistInDocument())
                    {
                        AddinServer.AppAutomation.CreateReferences(dwgDoc, AddinServer.Settings.ViewReferenceSettings);
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
        internal static string GetViewCalloutTextFromLabelText(this string labelText)
        {
            //DETAIL <DrawingViewName/> 

            int ViewLength = ("<DrawingViewName/>").Length;

            return labelText.Substring(labelText.IndexOf("<DrawingViewName/>"), labelText.LastIndexOf("<DrawingViewName/>") - labelText.IndexOf("<DrawingViewName/>") + ViewLength);
            //return LabelText.Substring(LabelText.IndexOf("<DrawingViewName/>"), LabelText.LastIndexOf("<DrawingViewName/>") + ViewLength);
        }

        static void Get_DefaultViewText(out string detailText, out string sectionText, out string auxText)
        {
            DrawingDocument oDoc = (DrawingDocument)AddinServer.InventorApp.ActiveDocument;

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
