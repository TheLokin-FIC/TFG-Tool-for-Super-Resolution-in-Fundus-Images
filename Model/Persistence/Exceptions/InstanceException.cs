using System;

namespace Model.Persistence.Exceptions
{
    public abstract class InstanceException : Exception
    {
        public object Key { get; private set; }
        public string ClassName { get; private set; }

        protected InstanceException(string specificMessage, object key, string className) : base(specificMessage + " (key = '" + key + "' - className = '" + className + "')")
        {
            Key = key;
            ClassName = className;
        }
    }
}