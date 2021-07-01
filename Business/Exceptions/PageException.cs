using System;

namespace Business.Exceptions
{
    public class PageException : Exception
    {
        public Exception EncapsulatedException { get; }

        public PageException(Exception exception) : base(exception.Message)
        {
            EncapsulatedException = exception;
        }
    }
}