using System;

namespace ViewReference
{
    public class AddingViewReferencesException : AggregateException
    {
		public AddingViewReferencesException(Exception innerException) : base("Adding View References Failed", innerException)
		{

		}
    }
}