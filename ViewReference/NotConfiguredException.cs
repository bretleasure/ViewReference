using System;

namespace ViewReference
{
    public class NotConfiguredException : AggregateException
    {
		public NotConfiguredException() : base("View Reference is not configured")
		{

		}
    }
}