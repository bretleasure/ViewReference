using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewReference
{
    public class ViewReferenceSettings
    {
        public string CalloutStyle { get; set; }
        public string DetailViewLabelStyle { get; set; }
        public string SectionViewLabelStyle { get; set; }
        public string AuxiliaryViewLabelStyle { get; set; }
        public string ProjectedViewLabelStyle { get; set; }
        public bool AddReferencesToDetailViews { get; set; }
        public bool AddReferencesToSectionViews { get; set; }
        public bool AddReferencesToAuxiliaryViews { get; set; }
        public bool AddReferencesToProjectedViews { get; set; }

        public static ViewReferenceSettings Default => new ViewReferenceSettings
        {
            CalloutStyle = $"{AttributeTags.ViewName} ({AttributeTags.ViewSheetNumber})",
            DetailViewLabelStyle = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            SectionViewLabelStyle = $"{AttributeTags.ViewName}-{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            AuxiliaryViewLabelStyle = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            ProjectedViewLabelStyle = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            AddReferencesToDetailViews = true,
            AddReferencesToSectionViews = true,
            AddReferencesToAuxiliaryViews = false,
            AddReferencesToProjectedViews = false,
        };

    }
}
