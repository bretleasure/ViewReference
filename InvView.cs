using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using ViewReference.Extensions;

namespace ViewReference
{
    public class InvView
    {
        public InvView(DrawingView view, string calloutStyle, string viewLabelStyle)
        {
            View = view;
            ViewName = view.Name;
            ViewSheetNumber = view.Parent.GetSheetNumber();
            ViewSheetName = view.Parent.GetSheetName();
            ParentSheetNumber = view.ParentView.Parent.GetSheetNumber();
            ParentSheetName = view.ParentView.Parent.GetSheetName();
            CalloutStyle = calloutStyle;
            ViewLabelStyle = viewLabelStyle;
            LabelText = view.Label.FormattedText;
            Valid = true;
        }
        
		public InvView(DrawingView view)
		{
            if (view.ViewHasReferences())
            {
                try
                {
                    AttributeSet oAttribSet = view.AttributeSets[AddinGlobal.AttributeSetName];
                    View = view;
                    ViewName = oAttribSet[AttributeNames.ViewName].Value.ToString();
                    ViewSheetName = oAttribSet[AttributeNames.ViewSheetName].Value.ToString();
                    ViewSheetNumber = oAttribSet[AttributeNames.ViewSheetNumber].Value.ToString();
                    ParentSheetName = oAttribSet[AttributeNames.ParentSheetName].Value.ToString();
                    ParentSheetNumber = oAttribSet[AttributeNames.ParentSheetName].Value.ToString();

                    CalloutStyle = oAttribSet[AttributeNames.CalloutStyle].Value.ToString();
                    ViewLabelStyle = oAttribSet[AttributeNames.ViewLabelStyle].Value.ToString();
                    LabelText = oAttribSet[AttributeNames.LabelText].Value.ToString();
                    Valid = true;
                }
                catch (Exception ex)
                {
                    Valid = false;
                }
            }
            else
            {
                Valid = false;
            }
        }

		public DrawingView View { get; private set; }

		public string ViewName { get; private set; }

        public string ViewSheetNumber { get; private set; }
        public string ViewSheetName { get; private set; }

        public string ParentSheetNumber { get; private set; }
        public string ParentSheetName { get; private set; }

        public string LabelText { get; private set; }
        public string CalloutStyle { get; private set; }
        public string ViewLabelStyle { get; private set; }
        public bool Valid { get; private set; }

		public string ViewCalloutWithReferences => ReplaceAttributesWithValues(CalloutStyle);

        public string ViewLabelWithReferences
		{
			get
			{
                //Get portion of View label that contains the View Name
                string viewCalloutText = this.View.Label.FormattedText.GetViewCalloutTextFromLabelText();

                string newViewCalloutText = ReplaceAttributesWithValues(ViewLabelStyle);

                return this.View.Label.FormattedText.Replace(viewCalloutText, newViewCalloutText);
            }
		}

        #region Private Methods

        /// <summary>
        /// This takes the Attribute string declared by user and replaces the attributes with the values and returns as a string.
        /// </summary>
        /// <param name="startString"></param>
        /// <param name="iView"></param>
        /// <returns></returns>
        string ReplaceAttributesWithValues(string startString)
        {
            return startString.Replace(AttributeTags.ViewName, ViewName)
                .Replace(AttributeTags.ViewSheetNumber, ViewSheetNumber)
                .Replace(AttributeTags.ViewSheetName, ViewSheetName)
                .Replace(AttributeTags.ParentSheetNumber, ParentSheetNumber)
                .Replace(AttributeTags.ParentSheetName, ParentSheetName)
                .Replace(AttributeTags.Delim, "<Delimiter/>")
                .Replace(AttributeTags.Scale, "<DrawingViewScale/>");
        }

        #endregion






    }
}
