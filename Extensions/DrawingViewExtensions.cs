using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace ViewReference.Extensions
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

            oAttribSet.AssignAttributeValue(AttributeNames.ViewName, iView.ViewName);
            oAttribSet.AssignAttributeValue(AttributeNames.ViewSheetName, iView.ViewSheetName);
            oAttribSet.AssignAttributeValue(AttributeNames.ViewSheetNumber, iView.ViewSheetNumber);
            oAttribSet.AssignAttributeValue(AttributeNames.ParentSheetName, iView.ParentSheetName);
            oAttribSet.AssignAttributeValue(AttributeNames.ParentSheetNumber, iView.ParentSheetNumber);
            oAttribSet.AssignAttributeValue(AttributeNames.CalloutStyle, iView.CalloutStyle);
            oAttribSet.AssignAttributeValue(AttributeNames.ViewLabelStyle, iView.ViewLabelStyle);
            oAttribSet.AssignAttributeValue(AttributeNames.LabelText, iView.LabelText);

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
                if (iView.Valid)
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
                }
            }
            finally
            {
                view.ClearViewAttributes();
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
