using Business.Exceptions;
using SharedData.SuperResolution;

namespace Business.Services.SuperResolutionService
{
    public interface ISuperResolutionService
    {
        /// <exception cref="ModelNotFoundException"/>
        ModelDetails GetDetails(int modelId);

        /// <exception cref="InternalErrorException"/>
        byte[] UpscaleImage(ResolutionData resolutionData);
    }
}