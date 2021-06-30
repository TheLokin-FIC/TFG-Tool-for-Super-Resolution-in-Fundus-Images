using System;

namespace Business.Exceptions
{
    public class SuperResolutionModelNotFoundException : Exception
    {
        public int ModelId { get; private set; }
        public int UpscaleFactor { get; private set; }

        public SuperResolutionModelNotFoundException(int modelId, byte upscaleFactor) : base($"Super resolution model not found (modelId={modelId} - upscaleFactor={upscaleFactor})")
        {
            ModelId = modelId;
            UpscaleFactor = upscaleFactor;
        }
    }
}