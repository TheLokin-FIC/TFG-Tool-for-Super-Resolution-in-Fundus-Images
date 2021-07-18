using System;

namespace Business.Exceptions
{
    public class InternalErrorException : Exception
    {
        public Exception EncapsulatedException { get; }

        public InternalErrorException(Exception exception) : base("An internal error occurred on the server")
        {
            EncapsulatedException = exception;
        }
    }
}