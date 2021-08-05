using System;

namespace Business.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("User authentication fails")
        {
        }
    }
}