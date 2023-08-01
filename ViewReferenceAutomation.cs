using System;
using System.Linq;
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
        public Task CreateReferences(DrawingDocument dwgDoc) => CreateReferences(ViewReferenceSettings.Default, dwgDoc);
        public Task CreateReferences(ViewReferenceSettings settings, DrawingDocument dwgDoc)
        {
            if (settings == null)
            {
                return Task.FromException(new NotConfiguredException());
            }
            
            var views = dwgDoc.AllDrawingViews()
                .Where(v => v.ViewHasReferences() || v.GetAddReferences(settings));

            foreach (var view in views)
            {
                if (view.ParentView != null)
                {
                    //Step 1 - Remove Current References if they Exist
                    if (view.ViewHasReferences())
                    {
                        view.ResetView(new InvView(view)); 
                    }

                    //Step 2 - Create New References
                    if (view.GetAddReferences(settings))
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
                            return Task.FromException(new AddingViewReferencesException(ex));
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Task RemoveReferences(DrawingDocument dwgDoc)
        {
            foreach (var view in dwgDoc.AllDrawingViews())
            {
                view.ResetView(new InvView(view));
            }

            return Task.CompletedTask;
        }
    }
}