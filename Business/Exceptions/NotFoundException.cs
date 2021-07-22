using System;

namespace Business.Exceptions
{
    public class NotFoundException : Exception
    {
        public object[] KeyValues { get; }

        public NotFoundException(params object[] keyValues) : base($"Not found with keyValues = {keyValues}")
        {
            KeyValues = keyValues;
        }
    }
}