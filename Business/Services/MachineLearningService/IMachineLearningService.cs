using Business.Exceptions;
using DataTransfer.Output.Collections;
using DataTransfer.Output.MachineLearning;
using DataTransfer.Output.MachineLearning.SuperResolution;
using System.Collections.Generic;

namespace Business.Services.MachineLearningService
{
    public interface IMachineLearningService
    {
        /// <exception cref="PageException"/>
        Page<ModelDetails> GetModelPage(int pageSize, int pageIndex, string searchTerm);

        /// <exception cref="ModelNotFoundException"/>
        ResolutionModelDetails GetResolutionModelDetails(int modelId);

        /// <exception cref="SuperResolutionModelNotFoundException"/>
        /// <exception cref="InternalErrorException"/>
        byte[] UpscaleImage(int modelId, byte upscaleFactor, byte[] image);

        /// <exception cref="EmptyDatasetException"/>
        /// <exception cref="SuperResolutionModelNotFoundException"/>
        /// <exception cref="InternalErrorException"/>
        DatasetMetrics GenerateDatasetMetrics(int modelId, byte upscaleFactor, IList<byte[]> dataset);
    }
}