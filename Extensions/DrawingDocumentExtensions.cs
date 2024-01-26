using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewReference;

namespace Inventor
{
    internal static class DrawingDocumentExtensions
    {
        internal static List<DrawingView> AllDrawingViews(this DrawingDocument dwgDoc)
        {
            return dwgDoc.Sheets.Cast<Sheet>()
                .SelectMany(sh => sh.DrawingViews.Cast<DrawingView>())
                .ToList();
        }

        internal static bool ViewReferencesExistInDocument(this DrawingDocument dwgDoc)
        {
            return dwgDoc.AllDrawingViews().Any(v => v.ViewHasReferences());
        }

        public static Task AddViewReferences(this DrawingDocument dwgDoc)
            => AddinGlobal.Automation.CreateReferences(dwgDoc);

        public static Task AddViewReferences(this DrawingDocument dwgDoc, ViewReferenceSettings settings)
            => AddinGlobal.Automation.CreateReferences(dwgDoc, settings);

        public static Task RemoveViewReferences(this DrawingDocument dwgDoc)
            => AddinGlobal.Automation.RemoveReferences(dwgDoc);
    }
}
