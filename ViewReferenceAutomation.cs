using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using ViewReference.Exceptions;

namespace ViewReference
{
    public class ViewReferenceAutomation : AddInAutomation
    {
        public Task CreateReferences(DrawingDocument dwgDoc) => CreateReferences(dwgDoc, ViewReferenceSettings.Default);
        public Task CreateReferences(DrawingDocument dwgDoc, ViewReferenceSettings settings)
        {
            if (settings == null)
            {
                return Task.FromException(new NotConfiguredException());
            }
            
            var views = dwgDoc.AllDrawingViews()
                .Where(v => v.ViewHasReferences() || v.ShouldAddReferences(settings));

            var addRefsToViews = views
                .Select(v => CreateReferences(v, settings));
            
            return Task.WhenAll(addRefsToViews);
        }

        public Task CreateReferences(DrawingView view, ViewReferenceSettings settings)
        {
            if (view.ParentView != null)
            {
                //Step 1 - Remove Current References if they Exist
                if (view.ViewHasReferences())
                {
                    view.ResetView(new InvView(view));
                }

                //Step 2 - Create New References
                if (view.ShouldAddReferences(settings))
                {
                    try
                    {
                        var iView = new InvView(view, settings.CalloutStyle, view.GetReferenceLabelStyle(settings));

                        //View Callout
                        view.Name = iView.ViewCalloutWithReferences;

                        //View Label
                        view.Label.FormattedText = iView.ViewLabelWithReferences;

                        //Save View Attributes
                        view.SaveAttributesToView(iView);
                    }
                    catch (Exception ex)
                    {
                        return Task.FromException(new AddingViewReferencesException(view.Name, ex));
                    }
                }
            }
                
            return Task.CompletedTask;
        }

        public Task RemoveReferences(DrawingDocument dwgDoc)
        {
            var removeRefsFromViews = dwgDoc.AllDrawingViews()
                .Where(v => v.ViewHasReferences())
                .Select(RemoveReferences);

            return Task.WhenAll(removeRefsFromViews);
        }

        public Task RemoveReferences(DrawingView view)
        {
            return Task.Run(() => view.ResetView(new InvView(view)));
        }
    }
}