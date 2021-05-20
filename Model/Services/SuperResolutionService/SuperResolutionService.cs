using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Model.Persistence.Daos.SuperResolutionModelDao;
using Model.Persistence.Models;
using Model.Services.Exceptions;
using Model.Services.SuperResolutionService.Models;
using Model.Utils;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Model.Services.SuperResolutionService
{
    public class SuperResolutionService : ISuperResolutionService
    {
        private readonly ISuperResolutionModelDao superResolutionModelDao;
        private readonly ConcurrentDictionary<int, InferenceSession> sessions;

        public SuperResolutionService(ISuperResolutionModelDao superResolutionModelDao)
        {
            this.superResolutionModelDao = superResolutionModelDao;
            sessions = new ConcurrentDictionary<int, InferenceSession>();
        }

        /// <exception cref="ModelNotFoundException"/>
        public IList<SuperResolutionModelDetails> FindUpscaleFactors(string modelName)
        {
            // Get the different models associated with that name
            IList<SuperResolutionModel> superResolutionModels = superResolutionModelDao.FindByName(modelName);

            if (superResolutionModels.Any())
            {
                // Encapsulates the details associated with the model name
                IList<SuperResolutionModelDetails> superResolutionModelDetails = new List<SuperResolutionModelDetails>();
                foreach (SuperResolutionModel superResolutionModel in superResolutionModels)
                {
                    superResolutionModelDetails.Add(new SuperResolutionModelDetails(superResolutionModel.Id, superResolutionModel.Name, (byte)superResolutionModel.UpscaleFactor));
                }

                return superResolutionModelDetails.ToImmutableList();
            }
            else
            {
                throw new ModelNotFoundException(modelName);
            }
        }

        /// <exception cref="OnnxRuntimeException"/>
        public Bitmap UpscaleImage(int modelId, Bitmap image)
        {
            // Get the session for the model
            InferenceSession session = GetSession(modelId);

            // Converts the bitmap to a tensor
            Tensor<float> tensor = Converter.ConvertBitmapToFloatTensor(image);

            // Generate the input for the model
            IReadOnlyCollection<NamedOnnxValue> input = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", tensor)
            };

            // Run the model
            using (IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(input))
            {
                // Get the output from the model
                Tensor<float> output = results.First().AsTensor<float>();

                // Converts the tensor to a bitmap and returns it
                return Converter.ConvertFloatTensorToBitmap(output);
            }
        }

        private InferenceSession GetSession(int modelId)
        {
            // Look if the session is cached
            if (sessions.TryGetValue(modelId, out InferenceSession session))
            {
                return session;
            }
            else
            {
                // Configure the model options
                SessionOptions options = new SessionOptions();
                options.AppendExecutionProvider_CUDA(0);

                // Look up the model details
                SuperResolutionModel superResolutionModel = superResolutionModelDao.Find(modelId);

                // Create a session with the model
                session = new InferenceSession(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), superResolutionModel.Path), options);

                // Cache the session
                sessions.TryAdd(modelId, session);

                return session;
            }
        }
    }
}