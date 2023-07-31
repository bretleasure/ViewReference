﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

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
                    ViewName = oAttribSet["ViewName"].Value.ToString();
                    ViewSheetName = oAttribSet["ViewSheetName"].Value.ToString();
                    ViewSheetNumber = oAttribSet["ViewSheetNumber"].Value.ToString();
                    ParentSheetName = oAttribSet["ParentSheetName"].Value.ToString();
                    ParentSheetNumber = oAttribSet["ParentSheetName"].Value.ToString();

                    CalloutStyle = oAttribSet["CalloutStyle"].Value.ToString();
                    ViewLabelStyle = oAttribSet["ViewLabelStyle"].Value.ToString();
                    LabelText = oAttribSet["LabelText"].Value.ToString();
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
            return startString.Replace(AttributeNames.ViewName, ViewName)
                .Replace(AttributeNames.ViewSheetNumber, ViewSheetNumber)
                .Replace(AttributeNames.ViewSheetName, ViewSheetName)
                .Replace(AttributeNames.ParentSheetNumber, ParentSheetNumber)
                .Replace(AttributeNames.ParentSheetName, ParentSheetName)
                .Replace(AttributeNames.Delim, "<Delimiter/>")
                .Replace(AttributeNames.Scale, "<DrawingViewScale/>");
        }

        #endregion






    }
}
