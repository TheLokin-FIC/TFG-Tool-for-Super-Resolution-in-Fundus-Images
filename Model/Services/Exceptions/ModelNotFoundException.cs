using System;

namespace Model.Services.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public string Name { get; private set; }

        public ModelNotFoundException(string name) : base("Model not found (name=" + name + ")")
        {
            Name = name;
        }
    }
}