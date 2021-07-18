using System;

namespace Repository.Exceptions
{
    public class InstanceNotFoundException : Exception
    {
        public Type Entity { get; }
        public object[] KeyValues { get; }

        public InstanceNotFoundException(Type entity, params object[] keyValues) : base($"Instance not found (entity={entity} - key = {keyValues})")
        {
            Entity = entity;
            KeyValues = keyValues;
        }
    }
}