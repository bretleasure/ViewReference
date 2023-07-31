using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using ViewReference.Exceptions;
using ViewReference.Extensions;

namespace ViewReference
{
    public class ViewReferenceAutomation : AddInAutomation
    {
        public Task CreateReferences() => CreateReferences(ViewReferenceSettings.Default);
        public Task CreateReferences(ViewReferenceSettings settings)
        {
            if (settings == null)
            {
                return Task.FromException(new NotConfiguredException());
            }
            
            try
            {
                if (AddinGlobal.InventorApp.ActiveDocument is DrawingDocument dwgDoc)
                {
                    foreach (var view in dwgDoc.AllDrawingViews())
                    {
                        switch (view.ViewType)
                        {
                            case DrawingViewTypeEnum.kDetailDrawingViewType:
                                if (settings.DetailView)
                                {
                                    view.AddReferencesToView(settings.DetailLabelStyle);
                                }
                                break;
                            case DrawingViewTypeEnum.kSectionDrawingViewType:
                                if (settings.SectionView)
                                {
                                    view.AddReferencesToView(settings.SectionLabelStyle);
                                }
                                break;
                            case DrawingViewTypeEnum.kAuxiliaryDrawingViewType:
                                if (settings.AuxView)
                                {
                                    view.AddReferencesToView(settings.AuxLabelStyle);
                                }
                                break;
                            case DrawingViewTypeEnum.kProjectedDrawingViewType:
                                if (settings.ProjectedView)
                                {
                                    view.AddReferencesToView(settings.ProjectedLabelStyle);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Task.FromException(new AddingViewReferencesException(ex));
            }
            
            return Task.CompletedTask;
        }

        public Task RemoveReferences()
        {
            if (AddinGlobal.InventorApp.ActiveDocument is DrawingDocument dwgDoc)
            {
                foreach (var view in dwgDoc.AllDrawingViews())
                {
                    view.ResetView(new InvView(view));
                }
            }

            return Task.CompletedTask;
        }
    }
}