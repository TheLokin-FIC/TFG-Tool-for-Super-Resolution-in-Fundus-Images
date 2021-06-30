using Business.Exceptions;
using SharedData.SuperResolution;

namespace Business.Services.SuperResolutionService
{
    public interface ISuperResolutionService
    {
        /// <exception cref="ModelNotFoundException"/>
        ResolutionDetails GetDetails(int modelId);

        /// <exception cref="SuperResolutionModelNotFoundException"/>
        /// <exception cref="InternalErrorException"/>
        byte[] UpscaleImage(int modelId, byte upscaleFactor, byte[] image);
    }
}