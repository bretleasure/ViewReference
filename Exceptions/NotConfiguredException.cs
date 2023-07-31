using System;

namespace ViewReference.Exceptions
{
    public class NotConfiguredException : AggregateException
    {
        public NotConfiguredException() : base("View Reference is not configured")
        {

        }
    }
}