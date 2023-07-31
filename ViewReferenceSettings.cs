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
        public string DetailLabelStyle { get; set; }
        public string SectionLabelStyle { get; set; }
        public string AuxLabelStyle { get; set; }
        public string ProjectedLabelStyle { get; set; }
        public bool DetailView { get; set; }
        public bool SectionView { get; set; }
        public bool AuxView { get; set; }
        public bool ProjectedView { get; set; }
        public bool UpdateBeforeSave { get; set; }

        public static ViewReferenceSettings Default => new ViewReferenceSettings
        {
            CalloutStyle = $"{AttributeNames.ViewName} ({AttributeNames.ViewSheetNumber})",
            DetailLabelStyle = $"{AttributeNames.ViewName} ({AttributeNames.ParentSheetNumber})",
            SectionLabelStyle = $"{AttributeNames.ViewName}-{AttributeNames.ViewName} ({AttributeNames.ParentSheetNumber})",
            AuxLabelStyle = $"{AttributeNames.ViewName} ({AttributeNames.ParentSheetNumber})",
            ProjectedLabelStyle = $"{AttributeNames.ViewName} ({AttributeNames.ParentSheetNumber})",
            DetailView = true,
            SectionView = true,
            AuxView = false,
            ProjectedView = false,
            UpdateBeforeSave = false
        };

    }
}
