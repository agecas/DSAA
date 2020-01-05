using System;

namespace DSAA.Shared
{
    public abstract class DssaException : Exception
    {
        protected DssaException(string message) : base(message)
        {
        }

        protected DssaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
