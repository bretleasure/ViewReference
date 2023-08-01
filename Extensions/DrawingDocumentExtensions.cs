using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace ViewReference.Extensions
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
    }
}
