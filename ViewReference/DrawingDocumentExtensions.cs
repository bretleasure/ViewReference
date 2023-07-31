using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace ViewReference
{
	public static class DrawingDocumentExtensions
	{
        public static List<DrawingView> AllDrawingViews(this DrawingDocument dwgDoc)
        {
            return dwgDoc.Sheets.Cast<Sheet>()
                .SelectMany(sh => sh.DrawingViews.Cast<DrawingView>())
                .ToList();
        }

        public static bool ViewReferencesExistInDocument(this DrawingDocument dwgDoc)
        {
            return dwgDoc.AllDrawingViews().Any(v => v.ViewHasReferences());
        }
    }
}
