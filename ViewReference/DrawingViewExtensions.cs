using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using RemoveOldViewReferences;
using RemoveOldViewReferences.RemoveOldViewReferences;

namespace ViewReference
{
	public static class DrawingViewExtensions
	{
        public static bool ViewHasReferences(this DrawingView view)
        {
            return view.AttributeSets.NameIsUsed[AddinGlobal.AttributeSetName];
        }

        public static void SaveAttributesToView(this DrawingView dwgView, InvView iView)
        {
            //AddinGlobal.Logger.LogInformation($"Saving attributes to view");

            if (!dwgView.AttributeSets.NameIsUsed[AddinGlobal.AttributeSetName])
            {
                dwgView.AttributeSets.Add(AddinGlobal.AttributeSetName, true);
            }

            AttributeSet oAttribSet = dwgView.AttributeSets[AddinGlobal.AttributeSetName];

            oAttribSet.AssignAttributeValue("ViewName", iView.ViewName);
            oAttribSet.AssignAttributeValue("ViewSheetName", iView.ViewSheetName);
            oAttribSet.AssignAttributeValue("ViewSheetNumber", iView.ViewSheetNumber);
            oAttribSet.AssignAttributeValue("ParentSheetName", iView.ParentSheetName);
            oAttribSet.AssignAttributeValue("ParentSheetNumber", iView.ParentSheetNumber);
            oAttribSet.AssignAttributeValue("CalloutStyle", iView.CalloutStyle);
            oAttribSet.AssignAttributeValue("ViewLabelStyle", iView.ViewLabelStyle);
            oAttribSet.AssignAttributeValue("LabelText", iView.LabelText);

        }

        public static void AddReferencesToView(this DrawingView view, string labelStyle)
        {
            try
            {
                if (view.ParentView != null)
                {
                    var currentRefs = new InvView(view);

                    //Step 1 - Remove Current References if they Exist
                    view.ResetView(currentRefs);

                    //Step 2 - Create New References                    
                    view.CreateViewReferences(labelStyle); ;
                }

            }
            catch (Exception ex)
            {
                //AddinGlobal.Logger.LogError(ex, $"Failed to add references to view {oView.Name}");
            }
        }

        public static void ResetView(this DrawingView view, InvView iView)
        {

            try
            {
                if (iView != null && iView.Valid)
                {
                    //Skip if references dont exist in the view label
                    //Can happen if labels were manually edited to remove the references
                    //and the attributes still exist in the view object
                    if (!view.Label.FormattedText.Contains("<DrawingViewName/>"))
                    {
                        //AddinGlobal.Logger.LogInformation($"Removing references from {oView.Name}");

                        view.Name = iView.ViewName;
                        view.Label.FormattedText = iView.LabelText;
                    }
                    else
                    {
                        //AddinGlobal.Logger.LogInformation($"{oView.Name} does not contain any references to remove.");
                    }
                }
                else
                {
                    if (view.OldReferencesExist())
                    {
                        //AddinGlobal.Logger.LogInformation($"View {oView.Name} contains old references. Resetting view now");

                        //Remove Old References
                        ViewRef_Remove OldVR = new ViewRef_Remove();
                        OldVR.Remove_ViewRefs(AddinGlobal.InventorApp);

                        //Remove Old ViewReference Attribute Set
                        if (view.AttributeSets.NameIsUsed["ViewReference"])
                            view.AttributeSets["ViewReference"].Delete();
                    }

                    //References were never added, or they were added as the original app

                }
            }
            finally
            {
                view.ClearViewAttributes();
            }

        }

        public static bool OldReferencesExist(this DrawingView view)
        {
            if (view.AttributeSets.NameIsUsed["ViewReference"])
                return true;
            else
            {
                if (!view.Label.FormattedText.Contains("<DrawingViewName/>"))
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

        public static void CreateViewReferences(this DrawingView view, string labelStyle)
        {
            var iView = new InvView(view, AddinGlobal.Settings.CalloutStyle, labelStyle);

            //View Callout
            view.Name = iView.ViewCalloutWithReferences;

            //View Label
            view.Label.FormattedText = iView.ViewLabelWithReferences;

            //Save View Attributes
            view.SaveAttributesToView(iView);
        }

        /// <summary>
        /// Clears All attributes from View by Deleting ViewReference Attribute Set
        /// </summary>
        /// <param name="view"></param>
        public static void ClearViewAttributes(this DrawingView view)
        {
            //AddinGlobal.Logger.LogInformation($"Clearing attributes for view {oView.Name}");

            view.AttributeSets.Cast<AttributeSet>()
                .Where(set => set.Name.ToLower().Contains("viewreference"))
                .ToList()
                .ForEach(set => set.Delete());
        }
    }
}
