using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewReference;

namespace Inventor
{
    public static class DrawingViewExtensions
    {
        internal static bool ViewHasReferences(this DrawingView view)
        {
            return view.AttributeSets.NameIsUsed[AddinServer.AttributeSetName];
        }

        internal static bool ShouldAddReferences(this DrawingView view, ViewReferenceSettings settings)
        {
            var shouldAddToViewType = view.ViewType switch
            {
                DrawingViewTypeEnum.kDetailDrawingViewType => settings.AddReferencesToDetailViews,
                DrawingViewTypeEnum.kSectionDrawingViewType => settings.AddReferencesToSectionViews,
                DrawingViewTypeEnum.kAuxiliaryDrawingViewType => settings.AddReferencesToAuxiliaryViews,
                DrawingViewTypeEnum.kProjectedDrawingViewType => settings.AddReferencesToProjectedViews,
                _ => false
            };
            
            var labelContainsDrawingViewName = view.Label.FormattedText.Contains("<DrawingViewName/>");
            
            return shouldAddToViewType && labelContainsDrawingViewName;
        }

        internal static string GetReferenceLabelStyle(this DrawingView view, ViewReferenceSettings settings)
        {
            return view.ViewType switch
            {
                DrawingViewTypeEnum.kDetailDrawingViewType => settings.DetailViewLabelStyle,
                DrawingViewTypeEnum.kSectionDrawingViewType => settings.SectionViewLabelStyle,
                DrawingViewTypeEnum.kAuxiliaryDrawingViewType => settings.AuxiliaryViewLabelStyle,
                DrawingViewTypeEnum.kProjectedDrawingViewType => settings.ProjectedViewLabelStyle,
                _ => string.Empty
            };
        }

        internal static void SaveAttributesToView(this DrawingView dwgView, InvView iView)
        {
            //AddinGlobal.Logger.LogInformation($"Saving attributes to view");

            if (!dwgView.AttributeSets.NameIsUsed[AddinServer.AttributeSetName])
            {
                dwgView.AttributeSets.Add(AddinServer.AttributeSetName, true);
            }

            AttributeSet oAttribSet = dwgView.AttributeSets[AddinServer.AttributeSetName];

            oAttribSet.AssignAttributeValue(AttributeNames.ViewName, iView.ViewName);
            oAttribSet.AssignAttributeValue(AttributeNames.ViewSheetName, iView.ViewSheetName);
            oAttribSet.AssignAttributeValue(AttributeNames.ViewSheetNumber, iView.ViewSheetNumber);
            oAttribSet.AssignAttributeValue(AttributeNames.ParentSheetName, iView.ParentSheetName);
            oAttribSet.AssignAttributeValue(AttributeNames.ParentSheetNumber, iView.ParentSheetNumber);
            oAttribSet.AssignAttributeValue(AttributeNames.CalloutStyle, iView.CalloutStyle);
            oAttribSet.AssignAttributeValue(AttributeNames.ViewLabelStyle, iView.ViewLabelStyle);
            oAttribSet.AssignAttributeValue(AttributeNames.LabelText, iView.LabelText);

        }

        internal static void ResetView(this DrawingView view, InvView iView)
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

        /// <summary>
        /// Clears All attributes from View by Deleting ViewReference Attribute Set
        /// </summary>
        /// <param name="view"></param>
        internal static void ClearViewAttributes(this DrawingView view)
        {
            //AddinGlobal.Logger.LogInformation($"Clearing attributes for view {oView.Name}");

            view.AttributeSets.Cast<AttributeSet>()
                .Where(set => set.Name.ToLower().Contains("viewreference"))
                .ToList()
                .ForEach(set => set.Delete());
        }

        public static Task AddViewReference(this DrawingView view)
            => AddinServer.AppAutomation.CreateReferences(view);
        
        public static Task AddViewReference(this DrawingView view, ViewReferenceSettings settings)
            => AddinServer.AppAutomation.CreateReferences(view, settings);

        public static Task RemoveViewReference(this DrawingView view)
            => AddinServer.AppAutomation.RemoveReferences(view);
    }
}
