using Microsoft.ML.OnnxRuntime;
using Model.Services.Exceptions;
using Model.Services.SuperResolutionService.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Model.Services.SuperResolutionService
{
    public interface ISuperResolutionService
    {
        /// <exception cref="ModelNotFoundException"/>
        IList<SuperResolutionModelDetails> FindUpscaleFactors(string modelName);

        /// <exception cref="OnnxRuntimeException"/>
        Bitmap UpscaleImage(int modelId, Bitmap input);
    }
}