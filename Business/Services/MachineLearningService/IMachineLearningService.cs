using Business.Exceptions;
using DataTransfer.Output.Collections;
using DataTransfer.Output.MachineLearning;
using DataTransfer.Output.MachineLearning.SuperResolution;
using System;
using System.Collections.Generic;

namespace Business.Services.MachineLearningService
{
    public interface IMachineLearningService
    {
        /// <exception cref="PageException"/>
        Page<ModelDetails> ModelPage(int pageSize, int pageIndex, string searchTerm);

        /// <exception cref="NotFoundException"/>
        ResolutionModelDetails ResolutionModelDetails(int modelId);

        /// <exception cref="NotFoundException"/>
        /// <exception cref="InternalErrorException"/>
        byte[] UpscaleImage(int modelId, byte upscaleFactor, byte[] image);

        /// <exception cref="ArgumentException"/>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="InternalErrorException"/>
        DatasetMetrics GenerateDatasetMetrics(int modelId, byte upscaleFactor, IList<byte[]> dataset);
    }
}