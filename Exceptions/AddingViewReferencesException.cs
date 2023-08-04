using System;

namespace ViewReference.Exceptions
{
    public class AddingViewReferencesException : AggregateException
    {
        public string ViewName { get; }

        public AddingViewReferencesException(string viewName, Exception innerException) : base($"Adding View References Failed for view {viewName}", innerException)
        {
            ViewName = viewName;
        }
    }
}