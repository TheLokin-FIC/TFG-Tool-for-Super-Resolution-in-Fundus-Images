using System;

namespace Business.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public int ModelId { get; private set; }

        public ModelNotFoundException(int modelId) : base($"Model not found (modelId={modelId})")
        {
            ModelId = modelId;
        }
    }
}