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
            CalloutStyle = $"{AttributeTags.ViewName} ({AttributeTags.ViewSheetNumber})",
            DetailLabelStyle = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            SectionLabelStyle = $"{AttributeTags.ViewName}-{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            AuxLabelStyle = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            ProjectedLabelStyle = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})",
            DetailView = true,
            SectionView = true,
            AuxView = false,
            ProjectedView = false,
            UpdateBeforeSave = false
        };

    }
}
