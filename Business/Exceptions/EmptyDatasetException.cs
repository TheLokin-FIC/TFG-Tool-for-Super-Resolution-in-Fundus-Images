using System;

namespace Business.Exceptions
{
    public class EmptyDatasetException : Exception
    {
        public EmptyDatasetException() : base("The dataset cannot be empty")
        {
        }
    }
}